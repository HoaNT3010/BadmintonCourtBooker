using Application.ErrorHandlers;
using Application.RequestDTOs.MoMo;
using Application.RequestDTOs.Transaction;
using Application.ResponseDTOs.Booking;
using Application.ResponseDTOs.MoMo;
using Application.ResponseDTOs.Transaction;
using Application.Services.Interfaces;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Utilities.Paging;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Application.Services.ConcreteClasses
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService jwtService;
        private readonly IMapper mapper;
        private readonly IMoMoService moMoService;

        public TransactionService(IJwtService jwtService, IUnitOfWork unitOfWork, IMapper mapper, IMoMoService moMoService)
        {
            this.jwtService = jwtService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.moMoService = moMoService;
        }

        #region Query

        public async Task<TransactionSummary> GetPersonalFullTransaction(Guid transactionId)
        {
            var customerId = jwtService.GetCurrentUserId();
            var transaction = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(customerId, transactionId);
            if (transaction == null)
            {
                throw new NotFoundException($"Cannot found information of transaction with ID '{transactionId.ToString()}'");
            }
            return mapper.Map<TransactionSummary>(transaction);
        }

        public async Task<TransactionSummary> GetPersonalFullTransactionByDetail(int transactionDetailId)
        {
            var customerId = jwtService.GetCurrentUserId();
            var transaction = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(customerId, transactionDetailId);
            if (transaction == null)
            {
                throw new NotFoundException($"Cannot found information of transaction from transaction detail ID '{transactionDetailId.ToString()}'");
            }
            return mapper.Map<TransactionSummary>(transaction);
        }

        public async Task<PagedList<TransactionShortSummary>?> GetPersonalTransactions(TransactionQueryRequest queryRequest)
        {
            var creatorId = jwtService.GetCurrentUserId();

            Expression<Func<Transaction, bool>> filterExpression = GetTransactionFilterExpression(creatorId, queryRequest);
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> sortingExpression = GetSortingExpression(queryRequest.OrderBy, queryRequest.SortingOrder);

            var queryResult = await unitOfWork.TransactionRepository.GetPaginatedAsync(queryRequest.PageNumber,
                queryRequest.PageSize,
                filterExpression,
                sortingExpression);

            return mapper.Map<PagedList<TransactionShortSummary>>(queryResult);
        }

        #endregion

        #region ProcessTransaction

        public async Task<TransactionSummary> ProcessBookingTimeTransaction(Guid transactionId)
        {
            // Get current customer
            var customerId = jwtService.GetCurrentUserId();
            var customer = await unitOfWork.UserRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new BadRequestException("Failed to retrieved customer information!");
            }

            // Get transaction information
            var transaction = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(customerId, transactionId);
            if (transaction == null)
            {
                throw new BadRequestException($"Failed to retrieved information of transaction with ID: {transactionId}!");
            }
            if (transaction.Status != TransactionStatus.Pending)
            {
                throw new BadRequestException($"Transaction with ID: {transactionId} is not in PENDING status! Only PENDING transaction can be process!");
            }
            if (transaction.TotalBookingTime <= 0)
            {
                throw new BadRequestException($"Transaction with ID: {transactionId} is not a booking time transaction! Only booking time transaction can be process in this method!");
            }

            // Validate booking slot(s) of transaction
            var validateResult = await ValidateTransactionBookingSlots(transaction);
            if (!validateResult)
            {
                // If transaction 's booking slot and rent time already has successful booking
                // Cancel the transaction and transaction's booking
                await CancelTransactionAndBooking(transaction);
                throw new BadRequestException($"Failed to process transaction. The booking slot of the transaction has already been booked. The transaction and its booking has been set to CANCEL status.");
            }

            // Validate customer's booking time balance
            ValidateBookingTimeBalance(transaction.TotalBookingTime, customer.BookingTime);

            // Process transaction
            customer.BookingTime -= transaction.TotalBookingTime;
            transaction.Status = TransactionStatus.Success;

            try
            {
                await unitOfWork.BeginTransactionAsync();
                unitOfWork.UserRepository.Update(customer);
                unitOfWork.TransactionRepository.Update(transaction);
                await unitOfWork.SaveChangeAsync();
                // Update bookings of transaction
                foreach (var detail in transaction.TransactionDetails)
                {
                    var booking = await unitOfWork.BookingRepository.GetBookingByTransactionDetail(detail.Id);
                    if (booking == null)
                    {
                        throw new Exception($"Process transaction: Failed to retrieved information of booking with transaction detail ID: {detail.Id}");
                    }
                    booking.Status = BookingStatus.Success;
                    unitOfWork.BookingRepository.Update(booking);
                    await unitOfWork.SaveChangeAsync();
                }
                await unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"Error when trying to process transaction and booking: {ex.Message}");
            }
            var result = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(customer.Id, transaction.Id);
            return mapper.Map<TransactionSummary>(result);
        }

        public async Task<MoMoCreatePaymentResponse> CreateMoMoPaymentForBookingTransaction(Guid transactionId)
        {
            // Get current customer
            var customerId = jwtService.GetCurrentUserId();
            var customer = await unitOfWork.UserRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new BadRequestException("Failed to retrieved customer information!");
            }

            // Get transaction information
            var transaction = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(customerId, transactionId);
            if (transaction == null)
            {
                throw new BadRequestException($"Failed to retrieved information of transaction with ID: {transactionId}!");
            }
            if (transaction.Status != TransactionStatus.Pending)
            {
                throw new BadRequestException($"Transaction with ID: {transactionId} is not in PENDING status! Only PENDING transaction can be process!");
            }
            if (transaction.TotalAmount <= 0)
            {
                throw new BadRequestException($"Transaction with ID: {transactionId} is not a monetary transaction! Only monetary transaction can be process in this method!");
            }
            if (transaction.PaymentMethod != PaymentMethodType.MoMo)
            {
                throw new BadRequestException($"Transaction with ID: {transactionId} does not accept MoMo as a payment method! This endpoint only support MoMo payment method!");
            }

            // Validate booking slot(s) of transaction
            var validateResult = await ValidateTransactionBookingSlots(transaction);
            if (!validateResult)
            {
                // If transaction 's booking slot and rent time already has successful booking
                // Cancel the transaction and transaction's booking
                await CancelTransactionAndBooking(transaction);
                throw new BadRequestException($"Failed to process transaction. The booking slot of the transaction has already been booked. The transaction and its booking has been set to CANCEL status.");
            }

            var paymentMethod = moMoService.CreateMoMoPaymentForBookingTransaction(transaction);
            return paymentMethod != null ? paymentMethod : throw new Exception($"Unexpected error occurred when trying to obtain MoMo payment method!");
        }

        public async Task ProcessMoMoPaymentResponse(MoMoIpnRequest ipnRequest)
        {
            var extraData = JsonConvert.DeserializeObject<MoMoExtraData>(HashHelper.DecodeFromBase64(ipnRequest.extraData));
            if (extraData == null)
            {
                throw new Exception("Unable to received extra data from MoMo payment transaction!");
            }
            var transaction = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(Guid.Parse(extraData.CustomerId), Guid.Parse(extraData.TransactionId));
            if (transaction == null)
            {
                throw new BadRequestException($"[MoMo Ipn] Failed to retrieved information of transaction with ID: {extraData.TransactionId}!");
            }
            if (transaction.Status != TransactionStatus.Pending)
            {
                throw new BadRequestException($"[MoMo Ipn] Transaction with ID: {extraData.TransactionId} is not in PENDING status! Only PENDING transaction can be process!");
            }
            if (extraData.TransactionType == TransactionDetailType.CourtBooking.ToString())
            {
                await HandleCourtBookingIpn(ipnRequest, transaction);
            }
            else if (extraData.TransactionType == TransactionDetailType.BookingTimeRecharge.ToString())
            {
                await HandleBookingTimeIpn(ipnRequest, transaction);
            }
        }

        #endregion

        #region ProcessRechargeTransaction

        public async Task<MoMoCreatePaymentResponse> HandleBookingTimeRechargeRequest(BookingTimeRechargeRequest rechargeRequest)
        {
            // Get current customer
            var customerId = jwtService.GetCurrentUserId();
            var customer = await unitOfWork.UserRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new BadRequestException("Failed to retrieved customer information!");
            }

            var hasPendingRechargeTransaction = await unitOfWork.TransactionRepository.IsAnyPendingRechargeTransaction(customerId);
            if (hasPendingRechargeTransaction)
            {
                throw new BadRequestException($"Customer '{customerId.ToString()}' already has a pending booking time recharge transaction!");
            }

            Guid newTransactionId = Guid.NewGuid();
            Transaction transaction = new Transaction()
            {
                Id = newTransactionId,
                Account = null,
                TotalAmount = rechargeRequest.RechargeAmount * 100000,
                TotalBookingTime = rechargeRequest.RechargeAmount,
                TransactionCode = null,
                PaymentMethod = PaymentMethodType.MoMo,
                Status = TransactionStatus.Pending,
                CreatorId = customerId,
            };

            TransactionDetail transactionDetail = new TransactionDetail()
            {
                Description = $"Customer '{customerId.ToString()}' recharge booking time to their account with amount: {rechargeRequest.RechargeAmount}",
                Amount = transaction.TotalAmount,
                BookingTime = transaction.TotalBookingTime,
                Type = TransactionDetailType.BookingTimeRecharge,
                TransactionId = newTransactionId,
            };
            transaction.TransactionDetails.Add(transactionDetail);

            try
            {
                await unitOfWork.TransactionRepository.AddAsync(transaction);
                var addResult = await unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Unexpected error occurred when trying to add new booking time recharge transaction: {ex.Message}");
            }

            var paymentMethod = moMoService.CreateMoMoPaymentForRechargeTransaction(transaction);
            return paymentMethod != null ? paymentMethod : throw new Exception($"Unexpected error occurred when trying to obtain MoMo payment method for booking time recharge request!");
        }

        #endregion

        #region Cancel

        public async Task<TransactionSummary> CancelTransaction(Guid transactionId)
        {
            var customerId = jwtService.GetCurrentUserId();
            var transaction = await unitOfWork.TransactionRepository.GetCustomerFullTransaction(customerId, transactionId);
            if (transaction == null)
            {
                throw new NotFoundException($"Cannot found information of transaction with ID '{transactionId.ToString()}'");
            }
            if (transaction.TransactionDetails.Any(td => td.Type == TransactionDetailType.CourtBooking))
            {
                await CancelTransactionAndBooking(transaction);
            }
            else
            {
                await CancelTransaction(transaction);
            }
            return mapper.Map<TransactionSummary>(transaction);
        }

        #endregion

        #region Utilities

        private Expression<Func<Transaction, bool>> GetTransactionFilterExpression(Guid? customerId, TransactionQueryRequest queryRequest)
        {
            Expression<Func<Transaction, bool>> baseExp = (t => t.CreatorId == customerId);
            Expression<Func<Transaction, bool>>? statusExp = queryRequest.Status == TransactionStatus.None ? null : (t => t.Status == queryRequest.Status);
            Expression<Func<Transaction, bool>>? methodTypeExp = queryRequest.MethodType == PaymentMethodType.None ? null : (t => t.PaymentMethod == queryRequest.MethodType);
            Expression<Func<Transaction, bool>>? typeExp = queryRequest.Type == TransactionDetailType.None ? null : (t => t.TransactionDetails.Any(td => td.Type == queryRequest.Type));

            ParameterExpression parameter = Expression.Parameter(typeof(Transaction));
            Expression combinedBody = Expression.Invoke(baseExp, parameter);

            if (statusExp != null)
            {
                combinedBody = Expression.AndAlso(combinedBody, Expression.Invoke(statusExp, parameter));
            }

            if (methodTypeExp != null)
            {
                combinedBody = Expression.AndAlso(combinedBody, Expression.Invoke(methodTypeExp, parameter));
            }

            if (typeExp != null)
            {
                combinedBody = Expression.AndAlso(combinedBody, Expression.Invoke(typeExp, parameter));
            }

            return Expression.Lambda<Func<Transaction, bool>>(combinedBody, parameter);
        }

        private Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> GetSortingExpression(TransactionOrderBy orderBy, SortingOrder sortingOrder)
        {
            //var orderings = new Dictionary<(TransactionOrderBy, SortingOrder), Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>>>
            //{
            //    {(TransactionOrderBy.CreateDate, SortingOrder.Ascending), q => q.OrderBy(t => t.CreatedDate)},
            //    {(TransactionOrderBy.TotalAmount, SortingOrder.Ascending), q => q.OrderBy(t => t.TotalAmount)},
            //    {(TransactionOrderBy.TotalBookingTime, SortingOrder.Ascending), q => q.OrderBy(t => t.TotalBookingTime)},
            //    {(TransactionOrderBy.CreateDate, SortingOrder.Descending), q => q.OrderByDescending(t => t.CreatedDate)},
            //    {(TransactionOrderBy.TotalAmount, SortingOrder.Descending), q => q.OrderByDescending(t => t.TotalAmount)},
            //    {(TransactionOrderBy.TotalBookingTime, SortingOrder.Descending), q => q.OrderByDescending(t => t.TotalBookingTime)},
            //};

            //if (orderings.TryGetValue((orderBy, sortingOrder), out var sortingExpression))
            //{
            //    return sortingExpression;
            //}

            //return (q => q.OrderBy(t => t.CreatedDate));

            switch (orderBy)
            {
                case TransactionOrderBy.CreateDate:
                    return sortingOrder == SortingOrder.Ascending ? (q => q.OrderBy(t => t.CreatedDate)) : (q => q.OrderByDescending(t => t.CreatedDate));
                case TransactionOrderBy.TotalAmount:
                    return sortingOrder == SortingOrder.Ascending ? (q => q.OrderBy(t => t.TotalAmount)) : (q => q.OrderByDescending(t => t.TotalAmount));
                case TransactionOrderBy.TotalBookingTime:
                    return sortingOrder == SortingOrder.Ascending ? (q => q.OrderBy(t => t.TotalBookingTime)) : (q => q.OrderByDescending(t => t.TotalBookingTime));
            }

            return (q => q.OrderByDescending(t => t.CreatedDate));
        }

        private void ValidateBookingTimeBalance(decimal totalBookingTime, decimal bookingTimeBalance)
        {
            if (bookingTimeBalance <= 0)
            {
                throw new BadRequestException($"Customer does not have any booking time. Please purchase more!");
            }
            if (bookingTimeBalance < totalBookingTime)
            {
                throw new BadRequestException($"Insufficient booking time balance. Current amount: {bookingTimeBalance} - Required amount: {totalBookingTime} - Missing amount: {totalBookingTime - bookingTimeBalance}!");
            }
        }

        private async Task<bool> ValidateTransactionBookingSlots(Transaction transaction)
        {
            foreach (var detail in transaction.TransactionDetails)
            {
                var detailBooking = await unitOfWork.BookingRepository.GetBookingByTransactionDetail(detail.Id);
                if (detailBooking == null)
                {
                    throw new NotFoundException("Failed to retrieved booking information of the transaction!");
                }
                var hasSuccessBooking = await unitOfWork.BookingRepository.IsAnySuccessBookings(detailBooking.RentDate, (int)detailBooking.SlotId!);
                if (hasSuccessBooking)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task CancelTransactionAndBooking(Transaction transaction)
        {
            if (transaction.Status == TransactionStatus.Cancel) { return; }
            try
            {
                await unitOfWork.BeginTransactionAsync();
                // Update transaction's status
                transaction.Status = TransactionStatus.Cancel;
                unitOfWork.TransactionRepository.Update(transaction);
                await unitOfWork.SaveChangeAsync();

                // Update bookings' status from each transaction detail
                if (transaction.TransactionDetails == null || transaction.TransactionDetails.Count == 0)
                {
                    throw new Exception($"Cancel transaction: Failed to retrieved transaction detail information of transaction.");
                }
                foreach (var detail in transaction.TransactionDetails)
                {
                    var booking = await unitOfWork.BookingRepository.GetBookingByTransactionDetail(detail.Id);
                    if (booking == null)
                    {
                        throw new Exception($"Cancel transaction: Failed to retrieved information of booking with transaction detail ID: {detail.Id}");
                    }
                    booking.Status = BookingStatus.Cancel;
                    unitOfWork.BookingRepository.Update(booking);
                    await unitOfWork.SaveChangeAsync();
                }
                await unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"Error when trying to cancel transaction and booking: {ex.Message}");
            }
        }

        private async Task CancelAllPendingBookings(Transaction successfulTransaction)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync();

                foreach (var detail in successfulTransaction.TransactionDetails)
                {
                    var detailBooking = await unitOfWork.BookingRepository.GetBookingByTransactionDetail(detail.Id);
                    var pendingBookings = await unitOfWork.BookingRepository.GetPendingBookings(detailBooking.RentDate, (int)detailBooking.SlotId);
                    if (pendingBookings == null || pendingBookings.Count == 0)
                    {
                        continue;
                    }
                    foreach (var booking in pendingBookings)
                    {
                        booking.Status = BookingStatus.Cancel;
                        unitOfWork.BookingRepository.Update(booking);
                    }
                    await unitOfWork.SaveChangeAsync();
                }

                await unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"Error when trying to cancel all pending bookings: {ex.Message}");
            }

        }

        private async Task HandleCourtBookingIpn(MoMoIpnRequest ipnRequest, Transaction transaction)
        {
            // MoMo payment transaction failed, cancel transaction and bookings
            transaction.TransactionCode = ipnRequest.transId.ToString();
            if (ipnRequest.resultCode != 0)
            {

                await CancelTransactionAndBooking(transaction);
            }
            // MoMo payment transaction success, change status of transaction and bookings to success
            else
            {
                // Process transaction
                transaction.Status = TransactionStatus.Success;
                try
                {
                    await unitOfWork.BeginTransactionAsync();
                    unitOfWork.TransactionRepository.Update(transaction);
                    await unitOfWork.SaveChangeAsync();
                    // Update bookings of transaction
                    foreach (var detail in transaction.TransactionDetails)
                    {
                        var booking = await unitOfWork.BookingRepository.GetBookingByTransactionDetail(detail.Id);
                        if (booking == null)
                        {
                            throw new Exception($"Process transaction from MoMo ipn: Failed to retrieved information of booking with transaction detail ID: {detail.Id}");
                        }
                        booking.Status = BookingStatus.Success;
                        unitOfWork.BookingRepository.Update(booking);
                        await unitOfWork.SaveChangeAsync();
                    }
                    await unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    await unitOfWork.RollbackAsync();
                    throw new Exception($"Error when trying to process transaction and booking with MoMo Ipn: {ex.Message}");
                }
            }
        }

        private async Task HandleBookingTimeIpn(MoMoIpnRequest ipnRequest, Transaction transaction)
        {
            // MoMo payment transaction failed, cancel transaction and bookings
            transaction.TransactionCode = ipnRequest.transId.ToString();
            if (ipnRequest.resultCode != 0)
            {
                await CancelTransaction(transaction);
            }
            // MoMo payment transaction success, change status of transaction to success and add booking time to customer's account
            else
            {
                // Process transaction
                transaction.Status = TransactionStatus.Success;
                try
                {
                    await unitOfWork.BeginTransactionAsync();
                    unitOfWork.TransactionRepository.Update(transaction);
                    await unitOfWork.SaveChangeAsync();
                    // Update customer's booking time
                    var customer = await unitOfWork.UserRepository.GetByIdAsync(transaction.CreatorId!);
                    if (customer == null)
                    {
                        throw new Exception($"Process transaction from MoMo ipn: Failed to retrieved customer with ID '{transaction.CreatorId}' information!");
                    }
                    customer.BookingTime += transaction.TotalBookingTime;
                    unitOfWork.UserRepository.Update(customer);
                    await unitOfWork.SaveChangeAsync();
                    await unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    await unitOfWork.RollbackAsync();
                    throw new Exception($"Error when trying to process booking time recharge transaction with MoMo Ipn: {ex.Message}");
                }
            }
        }

        private async Task CancelTransaction(Transaction transaction)
        {
            if (transaction.Status == TransactionStatus.Cancel) { return; }
            try
            {
                // Update transaction's status
                transaction.Status = TransactionStatus.Cancel;
                unitOfWork.TransactionRepository.Update(transaction);
                await unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when trying to cancel transaction: {ex.Message}");
            }
        }

        #endregion
    }
}
