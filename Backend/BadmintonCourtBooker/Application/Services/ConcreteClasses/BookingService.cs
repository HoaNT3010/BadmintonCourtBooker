using Application.ErrorHandlers;
using Application.RequestDTOs.Booking;
using Application.ResponseDTOs.Booking;
using Application.Services.Interfaces;
using AutoMapper;
using Infrastructure.Data.UnitOfWork;
using Domain.Enums;
using Application.Utilities;
using Domain.Entities;
using Application.ResponseDTOs.Transaction;

namespace Application.Services.ConcreteClasses
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.jwtService = jwtService;
        }

        #region SingularBooking

        public async Task<CreateBookingResponse?> CreateSingularBooking(Guid courtId, CreateBookingRequest bookingRequest)
        {
            // Validate court information
            var bookingCourt = await ValidateBookingCourt(courtId);

            // Validate court's slot
            var bookingSlot = await ValidateBookingSlot(bookingRequest.SlotId);

            // Validate court's booking method
            var bookingMethod = await ValidateBookingMethod(bookingRequest.BookingMethodId);
            if (bookingMethod.MethodType == BookingMethodType.Fixed)
            {
                throw new BadRequestException($"This feature only support create booking for Day and Flexible booking method. Please use different feature to create booking for Fixed booking method!");
            }

            // Validate court's payment method
            var paymentMethod = await ValidatePaymentMethod(bookingRequest.PaymentMethodId);

            // Check user ID
            Guid customerId = jwtService.GetCurrentUserId();

            // Validate booking information of rent date and slot time
            // Check for any confirmed/success booking
            // Check if current customer have any existing pending/success booking
            await ValidateBookingDetail((DateTime)bookingRequest.GetRentDate()!, bookingRequest.SlotId, customerId);

            // Create new booking
            Guid bookingId = Guid.NewGuid();
            Guid transactionId = Guid.NewGuid();

            Booking newBooking = new Booking()
            {
                Id = bookingId,
                Status = BookingStatus.Pending,
                RentDate = (DateTime)bookingRequest.GetRentDate()!,
                CourtId = courtId,
                SlotId = bookingRequest.SlotId,
                BookingMethodId = bookingRequest.BookingMethodId,
                CustomerId = customerId,
            };

            // Create booking's transaction and transaction detail
            Transaction newTransaction = new Transaction()
            {
                Id = transactionId,
                Account = null,
                TransactionCode = null,
                PaymentMethod = paymentMethod.MethodType,
                Status = TransactionStatus.Pending,
                CreatorId = customerId,
            };

            TransactionDetail newTransactionDetail = new TransactionDetail()
            {
                Description = GenerateBookingTransactionDetailDescription(customerId, courtId, bookingRequest.RentDate, bookingSlot),
                Type = TransactionDetailType.CourtBooking,
                TransactionId = transactionId,
            };

            if (bookingMethod.MethodType == BookingMethodType.Day)
            {
                newTransactionDetail.Amount = bookingMethod.PricePerSlot;
            }
            else if (bookingMethod.MethodType == BookingMethodType.Flexible)
            {
                newTransactionDetail.BookingTime = bookingMethod.TimePerSlot;
            }

            newTransaction.TransactionDetails.Add(newTransactionDetail);
            newTransaction.TotalAmount = newTransactionDetail.Amount;
            newTransaction.TotalBookingTime = newTransactionDetail.BookingTime;

            try
            {
                await unitOfWork.BeginTransactionAsync();
                var transactionResult = await unitOfWork.TransactionRepository.AddReturnEntityAsync(newTransaction);
                await unitOfWork.SaveChangeAsync();
                newBooking.TransactionDetailId = transactionResult!.TransactionDetails.First().Id;
                await unitOfWork.BookingRepository.AddAsync(newBooking);
                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();

                var bookingTransaction = mapper.Map<BookingTransaction>(transactionResult);
                var bookingCourtResult = new BookingCourt()
                {
                    Id = courtId.ToString(),
                    Name = bookingCourt.Name,
                    PhoneNumber = bookingCourt.PhoneNumber,
                    Address = bookingCourt.Address,
                };
                var bookingResult = mapper.Map<CreateBookingResponse>(newBooking);
                bookingResult.StartTime = DateTimeHelper.FormatTime(bookingSlot.StartTime);
                bookingResult.EndTime = DateTimeHelper.FormatTime(bookingSlot.EndTime);
                bookingResult.BookingMethod = bookingMethod.MethodType.ToString();
                bookingResult.Transaction = bookingTransaction;
                bookingResult.Court = bookingCourtResult;

                return bookingResult;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"An unexpected error occurred when trying to create new booking: {ex.Message}");
            }
        }

        #endregion

        #region Validation

        private async Task<Court> ValidateBookingCourt(Guid courtId)
        {
            var court = await unitOfWork.CourtRepository.GetByIdAsync(courtId);

            if (court == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court with ID: {courtId}");
            }

            if (court.CourtStatus != CourtStatus.Active)
            {
                throw new BadRequestException($"Failed to create booking for badminton court with ID: {courtId}. The court status is not ACTIVE!");
            }

            return court;
        }

        private async Task ValidateBookingDetail(DateTime rentDate, int slotId, Guid customerId)
        {
            var bookings = await unitOfWork.BookingRepository.GetBookings(rentDate, slotId);

            if (bookings == null || bookings.Count <= 0)
            {
                return;
            }

            foreach (var booking in bookings)
            {
                if ((booking.Status == BookingStatus.Pending || booking.Status == BookingStatus.Success) && booking.CustomerId == customerId)
                {
                    throw new BadRequestException($"Failed to create booking. Customer have already create booking for the badminton court from {DateTimeHelper.FormatTime(booking.Slot!.StartTime)} to {DateTimeHelper.FormatTime(booking.Slot!.EndTime)}, of date {DateTimeHelper.FormatDate(rentDate)}!");
                }
                else if (booking.Status == BookingStatus.Success)
                {
                    throw new BadRequestException($"Failed to create booking. The slot with ID: {slotId} from {DateTimeHelper.FormatTime(booking.Slot!.StartTime)} to {DateTimeHelper.FormatTime(booking.Slot!.EndTime)}, of date {DateTimeHelper.FormatDate(rentDate)} already have confirmed booking!");
                }
            }
        }

        private async Task<BookingMethod> ValidateBookingMethod(int bookingMethodId)
        {
            var bookingMethod = await unitOfWork.BookingMethodRepository.GetByIdAsync(bookingMethodId);

            if (bookingMethod == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court's booking method with ID: {bookingMethodId}");
            }

            if (bookingMethod.Status != BookingMethodStatus.Active)
            {
                throw new BadRequestException($"Failed to create booking. The booking method with ID: {bookingMethodId} is not ACTIVE!");
            }

            return bookingMethod;
        }

        private async Task<Slot> ValidateBookingSlot(int bookingSlotId)
        {
            var bookingSlot = await unitOfWork.SlotRepository.GetByIdAsync(bookingSlotId);

            if (bookingSlot == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court's slot with ID: {bookingSlotId}");
            }

            if (!bookingSlot.Available)
            {
                throw new BadRequestException($"Failed to create booking. The court's slot with ID: {bookingSlotId} is not available for booking!");
            }

            return bookingSlot;
        }

        private async Task<PaymentMethod> ValidatePaymentMethod(int paymentMethodId)
        {
            var paymentMethod = await unitOfWork.PaymentMethodRepository.GetByIdAsync(paymentMethodId);

            if (paymentMethod == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court's payment method with ID: {paymentMethodId}");
            }

            if (paymentMethod.Status != PaymentMethodStatus.Active)
            {
                throw new BadRequestException($"Failed to create booking. The court's payment method with ID: {paymentMethodId} is not ACTIVE!");
            }

            return paymentMethod;
        }

        #endregion

        #region UtilityMethods

        private string GenerateBookingTransactionDetailDescription(Guid userId, Guid courtId, string rentDate, Slot bookingSlot)
        {
            return $"User '{userId.ToString()}' create booking for badminton court '{courtId}' at {rentDate}, from {DateTimeHelper.FormatTime(bookingSlot.StartTime)} to {DateTimeHelper.FormatTime(bookingSlot.EndTime)}.";
        }

        #endregion
    }
}
