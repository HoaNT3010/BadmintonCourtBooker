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
using System.Security.Cryptography.Xml;

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
            var bookingSlot = await ValidateBookingSlot(bookingRequest.SlotId, bookingCourt);

            // Validate court's booking method
            var bookingMethod = await ValidateBookingMethod(bookingRequest.BookingMethodId, bookingCourt);
            if (bookingMethod.MethodType == BookingMethodType.Fixed)
            {
                throw new BadRequestException($"This feature only support create booking for Day and Flexible booking method. Please use different feature to create booking for Fixed booking method!");
            }

            // Validate court's payment method
            var paymentMethod = await ValidatePaymentMethod(bookingRequest.PaymentMethodId, bookingCourt);

            // Check user ID
            Guid customerId = jwtService.GetCurrentUserId();

            // Validate booking information of rent date and slot time
            // Check for any confirmed/success booking
            // Check if current customer have any existing pending/success booking
            await ValidateBookingDetail((DateTime)bookingRequest.GetRentDate()!, bookingSlot, customerId);

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

        #region MultipleBooking

        public async Task<CreateMultipleBookingResponse?> CreateMultipleBooking(Guid courtId, CreateMultipleBookingRequest bookingRequest)
        {
            // Validate court information
            var bookingCourt = await ValidateBookingCourt(courtId);

            // Validate court's slot
            var bookingSlot = await ValidateBookingSlot(bookingRequest.SlotId, bookingCourt);

            // Validate duration between rent dates
            await ValidateMultipleBookingDate(bookingRequest.RentDates, bookingSlot.ScheduleId);

            // Validate court's booking method
            var bookingMethod = await ValidateBookingMethod(bookingRequest.BookingMethodId, bookingCourt);
            if (bookingMethod.MethodType != BookingMethodType.Fixed)
            {
                throw new BadRequestException($"This feature only support create booking for Fixed booking method. Please use different feature to create booking for other booking method!");
            }

            // Validate court's payment method
            var paymentMethod = await ValidatePaymentMethod(bookingRequest.PaymentMethodId, bookingCourt);

            // Check user ID
            Guid customerId = jwtService.GetCurrentUserId();

            // Validate booking information of rent dates and slot time
            // Check for any confirmed/success booking
            // Check if current customer have any existing pending/success booking
            await ValidateMultipleBookingDetail(bookingRequest.RentDates, bookingRequest.SlotId, customerId);

            // Create new bookings
            var newBookings = GenerateMultipleBooking(bookingRequest, courtId, customerId);

            // Create bookings' transaction and transaction detail
            Guid transactionId = Guid.NewGuid();

            Transaction newTransaction = new Transaction()
            {
                Id = transactionId,
                Account = null,
                TransactionCode = null,
                PaymentMethod = paymentMethod.MethodType,
                Status = TransactionStatus.Pending,
                CreatorId = customerId,
            };
            GenerateMultipleTransactionDetails(bookingRequest, customerId, courtId, bookingSlot, newTransaction, bookingMethod);

            try
            {
                await unitOfWork.BeginTransactionAsync();
                var transactionResult = await unitOfWork.TransactionRepository.AddReturnEntityAsync(newTransaction);
                await unitOfWork.SaveChangeAsync();
                var transactionDetails = transactionResult!.TransactionDetails.ToList();
                for (int i = 0; i < newBookings.Count; i++)
                {
                    newBookings[i].TransactionDetailId = transactionDetails[i].Id;
                }
                await unitOfWork.BookingRepository.AddManyAsync(newBookings);
                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();

                // Mapping results
                var bookingTransaction = mapper.Map<BookingTransaction>(transactionResult);
                var bookingCourtResult = new BookingCourt()
                {
                    Id = courtId.ToString(),
                    Name = bookingCourt.Name,
                    PhoneNumber = bookingCourt.PhoneNumber,
                    Address = bookingCourt.Address,
                };

                List<MultipleBooking> bookingsResult = new List<MultipleBooking>();
                for (int i = 0; i < bookingRequest.RentDates.Count; i++)
                {
                    var newBooking = mapper.Map<MultipleBooking>(newBookings[i]);
                    newBooking.StartTime = DateTimeHelper.FormatTime(bookingSlot.StartTime);
                    newBooking.EndTime = DateTimeHelper.FormatTime(bookingSlot.EndTime);
                    newBooking.BookingMethod = bookingMethod.MethodType.ToString();

                    bookingsResult.Add(newBooking);
                }
                CreateMultipleBookingResponse result = new CreateMultipleBookingResponse()
                {
                    Bookings = bookingsResult,
                    Court = bookingCourtResult,
                    Transaction = bookingTransaction,
                };

                return result;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"An unexpected error occurred when trying to create new multiple booking: {ex.Message}");
            }
        }

        #endregion

        #region Validation

        private async Task<Court> ValidateBookingCourt(Guid courtId)
        {
            var court = await unitOfWork.CourtRepository.GetCourtFullDetail(courtId);

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

        private async Task ValidateBookingDetail(DateTime rentDate, Slot slot, Guid customerId)
        {
            var bookings = await unitOfWork.BookingRepository.GetPendingAndSuccessBookings(rentDate, slot.Id);

            if (bookings == null || bookings.Count <= 0)
            {
                return;
            }

            foreach (var booking in bookings)
            {
                if (booking.CustomerId == customerId)
                {
                    throw new BadRequestException($"Failed to create booking. Customer have already create booking for the badminton court from {DateTimeHelper.FormatTime(booking.Slot!.StartTime)} to {DateTimeHelper.FormatTime(booking.Slot!.EndTime)}, of date {DateTimeHelper.FormatDate(rentDate)}!");
                }
                else if (booking.Status == BookingStatus.Success)
                {
                    throw new BadRequestException($"Failed to create booking. The slot with ID: {slot.Id} from {DateTimeHelper.FormatTime(booking.Slot!.StartTime)} to {DateTimeHelper.FormatTime(booking.Slot!.EndTime)}, of date {DateTimeHelper.FormatDate(rentDate)} already have confirmed booking!");
                }
            }

            var schedule = await unitOfWork.ScheduleRepository.GetByIdAsync(slot.ScheduleId);
            if (schedule == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court's schedule with ID: {slot.ScheduleId}");
            }
            if (rentDate.DayOfWeek != schedule!.DayOfWeek)
            {
                throw new BadRequestException($"Failed to create booking. Rent date's day of week {DateTimeHelper.FormatDateWithName(rentDate)} does not match schedule's day of week {schedule.DayOfWeek.ToString()}!");
            }
        }

        private async Task<BookingMethod> ValidateBookingMethod(int bookingMethodId, Court court)
        {
            var bookingMethod = await unitOfWork.BookingMethodRepository.GetByIdAsync(bookingMethodId);

            if (bookingMethod == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court's booking method with ID: {bookingMethodId}");
            }

            if (!court.BookingMethods.Any(m => m.Id == bookingMethod.Id))
            {
                throw new BadRequestException($"Failed to create booking. The booking method with ID: {bookingMethodId} is not belong to court '{court.Id.ToString()}'!");
            }

            if (bookingMethod.Status != BookingMethodStatus.Active)
            {
                throw new BadRequestException($"Failed to create booking. The booking method with ID: {bookingMethodId} is not ACTIVE!");
            }

            if (bookingMethod.MethodType == BookingMethodType.None)
            {
                throw new BadRequestException($"Failed to create booking. Cannot create booking with booking method NONE!");
            }

            return bookingMethod;
        }

        private async Task<Slot> ValidateBookingSlot(int bookingSlotId, Court court)
        {
            var bookingSlot = await unitOfWork.SlotRepository.GetByIdAsync(bookingSlotId);

            if (bookingSlot == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court's slot with ID: {bookingSlotId}");
            }

            bool flag = true;
            foreach (var schedule in court.Schedules)
            {
                if (!schedule.Slots.Any(s => s.Id == bookingSlot.Id))
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                throw new BadRequestException($"Failed to create booking. The booking slot with ID: {bookingSlotId} is not belong to court '{court.Id.ToString()}'!");
            }

            if (!bookingSlot.Available)
            {
                throw new BadRequestException($"Failed to create booking. The court's slot with ID: {bookingSlotId} is not available for booking!");
            }

            return bookingSlot;
        }

        private async Task<PaymentMethod> ValidatePaymentMethod(int paymentMethodId, Court court)
        {
            var paymentMethod = await unitOfWork.PaymentMethodRepository.GetByIdAsync(paymentMethodId);

            if (paymentMethod == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court's payment method with ID: {paymentMethodId}");
            }

            if (!court.PaymentMethods.Any(p => p.Id == paymentMethod.Id))
            {
                throw new BadRequestException($"Failed to create booking. The payment method with ID: {paymentMethodId} is not belong to court '{court.Id.ToString()}'!");
            }

            if (paymentMethod.Status != PaymentMethodStatus.Active)
            {
                throw new BadRequestException($"Failed to create booking. The court's payment method with ID: {paymentMethodId} is not ACTIVE!");
            }

            return paymentMethod;
        }

        private async Task ValidateMultipleBookingDetail(List<BookingRentDate> rentDates, int slotId, Guid customerId)
        {
            foreach (var rentDate in rentDates)
            {
                var bookings = await unitOfWork.BookingRepository.GetPendingAndSuccessBookings((DateTime)rentDate.GetRentDate()!, slotId);

                if (bookings == null || bookings.Count <= 0)
                {
                    continue;
                }

                foreach (var booking in bookings)
                {
                    if (booking.CustomerId == customerId)
                    {
                        throw new BadRequestException($"Failed to create booking. Customer have already create booking for the badminton court from {DateTimeHelper.FormatTime(booking.Slot!.StartTime)} to {DateTimeHelper.FormatTime(booking.Slot!.EndTime)}, of date {DateTimeHelper.FormatDate(booking.RentDate)}!");
                    }
                    else if (booking.Status == BookingStatus.Success)
                    {
                        throw new BadRequestException($"Failed to create booking. The slot with ID: {slotId} from {DateTimeHelper.FormatTime(booking.Slot!.StartTime)} to {DateTimeHelper.FormatTime(booking.Slot!.EndTime)}, of date {DateTimeHelper.FormatDate(booking.RentDate)} already have confirmed booking!");
                    }
                }
            }
        }

        private async Task ValidateMultipleBookingDate(List<BookingRentDate> rentDates, int scheduleId)
        {
            List<DateTime> rentDateList = new List<DateTime>();
            foreach (var rentDate in rentDates)
            {
                rentDateList.Add((DateTime)rentDate.GetRentDate()!);
            }

            rentDateList.Sort(DateTime.Compare);
            var schedule = await unitOfWork.ScheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
            {
                throw new NotFoundException($"Cannot found information of badminton court's schedule with ID: {scheduleId}");
            }
            AreDatesSeparatedByOneWeek(rentDateList, schedule.DayOfWeek);
        }

        private void AreDatesSeparatedByOneWeek(List<DateTime> dates, System.DayOfWeek scheduleDay)
        {
            if (dates == null || dates.Count < 4)
            {
                throw new BadRequestException($"Failed to create booking. Total amount of rent dates in multiple booking must be more than 3!");
            }

            for (int i = 1; i < dates.Count; i++)
            {
                if (dates[i - 1].DayOfWeek != scheduleDay)
                {
                    throw new BadRequestException($"Failed to create booking. Rent date's day of week {DateTimeHelper.FormatDateWithName(dates[i - 1])} does not match schedule's day of week {scheduleDay.ToString()}!");
                }

                TimeSpan timeDifference = dates[i] - dates[i - 1];
                if (timeDifference.Days != 7)
                {
                    throw new BadRequestException($"Failed to create booking. Each date in the rent dates list must be separated by one week (7 days)!");
                }
            }
        }

        #endregion

        #region UtilityMethods

        private string GenerateBookingTransactionDetailDescription(Guid userId, Guid courtId, string rentDate, Slot bookingSlot)
        {
            return $"User '{userId.ToString()}' create booking for badminton court '{courtId}' at {rentDate}, from {DateTimeHelper.FormatTime(bookingSlot.StartTime)} to {DateTimeHelper.FormatTime(bookingSlot.EndTime)}.";
        }

        private List<Booking> GenerateMultipleBooking(CreateMultipleBookingRequest bookingRequest, Guid courtId, Guid customerId)
        {
            List<Booking> bookings = new List<Booking>();
            foreach (var rentDate in bookingRequest.RentDates)
            {
                Booking newBooking = new Booking()
                {
                    Id = Guid.NewGuid(),
                    Status = BookingStatus.Pending,
                    RentDate = (DateTime)rentDate.GetRentDate()!,
                    CourtId = courtId,
                    SlotId = bookingRequest.SlotId,
                    BookingMethodId = bookingRequest.BookingMethodId,
                    CustomerId = customerId,
                };
                bookings.Add(newBooking);
            }
            return bookings;
        }

        private void GenerateMultipleTransactionDetails(CreateMultipleBookingRequest bookingRequest, Guid customerId, Guid courtId, Slot bookingSlot, Transaction transaction, BookingMethod bookingMethod)
        {
            foreach (var rentDate in bookingRequest.RentDates)
            {
                TransactionDetail newTransactionDetail = new TransactionDetail()
                {
                    Description = GenerateBookingTransactionDetailDescription(customerId, courtId, rentDate.RentDate, bookingSlot),
                    Type = TransactionDetailType.CourtBooking,
                    TransactionId = transaction.Id,
                    Amount = bookingMethod.PricePerSlot,
                };

                transaction.TotalAmount += newTransactionDetail.Amount;
                transaction.TransactionDetails.Add(newTransactionDetail);
            }
        }

        #endregion
    }
}
