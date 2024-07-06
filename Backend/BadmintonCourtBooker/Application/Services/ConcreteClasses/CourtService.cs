using Application.ErrorHandlers;
using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;
using Application.Services.Interfaces;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ConcreteClasses
{
    public class CourtService : ICourtService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;

        public CourtService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.jwtService = jwtService;
        }

        #region Court

        public async Task<CourtCreateResponse?> CreateNewCourt(CourtCreateRequest createRequest)
        {
            // Get user info
            jwtService.CheckActiveAccountStatus();
            User? user = await unitOfWork.UserRepository.GetByIdAsync(jwtService.GetCurrentUserId());
            if (user == null)
            {
                throw new BadRequestException("Failed to get user data using bearer token!");
            }

            Court newCourt = mapper.Map<Court>(createRequest);
            newCourt.Id = Guid.NewGuid();
            newCourt.SlotDuration = AssignSlotDuration(createRequest.SlotType);
            newCourt.CourtStatus = CourtStatus.Inactive;
            newCourt.CreatorId = user.Id;

            // Add creator to court employee list as court manager
            newCourt.Employees = new List<Employee>()
            {
                new Employee()
                {
                    CourtId = newCourt.Id,
                    UserId = user.Id,
                    Role = EmployeeRole.Manager,
                    Status = EmployeeStatus.Active
                }
            };

            // Add default on-court payment method
            newCourt.PaymentMethods = new List<PaymentMethod>()
            {
                new PaymentMethod()
                {
                    MethodType = PaymentMethodType.OnCourt,
                    CourtId = newCourt.Id,
                    Account = "On-court Payment",
                }
            };

            try
            {
                await unitOfWork.CourtRepository.AddAsync(newCourt);
                int result = await unitOfWork.SaveChangeAsync();
                if (result == 0)
                {
                    throw new Exception("Unexpected error occurred when trying to add new court!");
                }
                var response = mapper.Map<CourtCreateResponse>(newCourt);
                response.CreatorFullName = user.LastName + " " + user.FirstName;
                response.CreatorEmail = user.Email;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourtDetail?> GetCourtDetail(Guid id)
        {
            var court = await unitOfWork.CourtRepository.GetCourtFullDetail(id);
            if (court == null)
            {
                throw new NotFoundException($"Cannot find badminton court with ID: {id.ToString()}");
            }
            return mapper.Map<CourtDetail>(court);
        }

        public async Task<bool> ActivateCourt(Guid id)
        {
            await CheckUserCourtEditingPermission(id);

            var court = await unitOfWork.CourtRepository.GetCourtFullDetail(id);

            if (court!.CourtStatus == CourtStatus.Active)
            {
                throw new BadRequestException($"Court with Id {id} is already activated!");
            }

            // Check conditions for court to be activated
            CheckCourtActivationConditions(court);

            court.CourtStatus = CourtStatus.Active;

            try
            {
                unitOfWork.CourtRepository.Update(court);
                var result = await unitOfWork.SaveChangeAsync();
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred when trying to activate court: {ex.Message}");
            }
        }

        private void CheckCourtActivationConditions(Court court)
        {
            // Must have at least 1 payment method
            if (court.PaymentMethods == null || court.PaymentMethods.Count <= 0)
            {
                throw new BadRequestException("Failed to activate court. The court must have at least 1 payment method!");
            }

            // Must have at least 1 booking method
            if (court.BookingMethods == null || court.BookingMethods.Count <= 0)
            {
                throw new BadRequestException("Failed to activate court. The court must have at least 1 booking method!");
            }

            // Must have court schedules
            if (court.Schedules == null || court.Schedules.Count <= 0)
            {
                throw new BadRequestException("Failed to activate court. The court does not have any schedule set up!");
            }

            // Each court schedule must have at least 1 slot set up
            foreach (var schedule in court.Schedules)
            {
                if (schedule.Slots == null || schedule.Slots.Count <= 0)
                {
                    throw new BadRequestException($"Failed to activate court. Court's schedule for {schedule.DayOfWeek} does not have any slot set up!");
                }
            }

            // Must have at least 1 employee with role manager
            if (court.Employees == null || court.Employees.Count <= 0)
            {
                throw new BadRequestException("Failed to activate court. The court does not have any employee!");
            }
            else if (!court.Employees.Any(e => e.Role == EmployeeRole.Manager))
            {
                throw new BadRequestException("Failed to activate court. The court does not have any employee with role of Manager!");
            }
        }

        public async Task<bool> DeactivateCourt(Guid id)
        {
            await CheckUserCourtEditingPermission(id);

            var court = await unitOfWork.CourtRepository.GetByIdAsync(id);

            if (court!.CourtStatus == CourtStatus.Inactive)
            {
                throw new BadRequestException($"Court with Id {id} is already deactivated!");
            }

            court.CourtStatus = CourtStatus.Inactive;

            try
            {
                unitOfWork.CourtRepository.Update(court);
                var result = await unitOfWork.SaveChangeAsync();
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred when trying to deactivate court: {ex.Message}");
            }
        }

        #endregion

        #region CourtSchedules

        public async Task<CourtDetail?> AddCourtSchedule(Guid id, CourtScheduleCreateRequest createRequest)
        {
            await CheckUserCourtEditingPermission(id);

            var court = await unitOfWork.CourtRepository.GetByIdAsync(id);

            // Check if court already has a schedule for any new schedule
            var courtSchedules = await unitOfWork.ScheduleRepository.GetCourtSchedules(court!.Id);
            CheckExistingCourtSchedules(courtSchedules!, createRequest.Schedules);

            // Validate court new schedules
            createRequest.CheckAndSwapOpenCloseTime();
            ValidateSchedulesDuration(court.SlotDuration, createRequest.Schedules);

            try
            {
                await unitOfWork.BeginTransactionAsync();
                foreach (var schedule in createRequest.Schedules)
                {
                    var newSchedule = await unitOfWork.ScheduleRepository.AddReturnEntityAsync(GenerateCourtSchedule(schedule, court.Id));
                    await unitOfWork.SaveChangeAsync();
                    var newSlots = GenerateScheduleSlots(newSchedule!, court.SlotDuration);
                    await unitOfWork.SlotRepository.AddManyAsync(newSlots);
                    await unitOfWork.SaveChangeAsync();
                }
                await unitOfWork.CommitAsync();
                var updatedCourt = await unitOfWork.CourtRepository.GetCourtFullDetail(court.Id);
                return mapper.Map<CourtDetail>(updatedCourt);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"An unexpected error occurred when trying to add court schedules: {ex.Message}");
            }
        }

        private Schedule GenerateCourtSchedule(CourtScheduleCreate schedule, Guid courtId)
        {
            return new Schedule()
            {
                DayOfWeek = schedule.DayOfWeek,
                OpenTime = (TimeSpan)DateTimeHelper.ConvertTimeString(schedule.OpenTime)!,
                CloseTime = (TimeSpan)DateTimeHelper.ConvertTimeString(schedule.CloseTime)!,
                CourtId = courtId,
            };
        }

        private List<Slot> GenerateScheduleSlots(Schedule schedule, TimeSpan slotDuration)
        {
            List<Slot> slots = new List<Slot>();

            var totalDurationTicks = (schedule.CloseTime - schedule.OpenTime).Ticks;
            var slotDurationTicks = slotDuration.Ticks;
            var slotCount = totalDurationTicks / slotDurationTicks;
            TimeSpan beginSlotTime = schedule.OpenTime;

            for (int i = 0; i < slotCount; i++)
            {
                Slot newSlot = new Slot()
                {
                    StartTime = beginSlotTime,
                    EndTime = beginSlotTime + slotDuration,
                    Available = true,
                    ScheduleId = schedule.Id,
                };
                slots.Add(newSlot);
                beginSlotTime += slotDuration;
            }

            return slots;
        }

        private void ValidateSchedulesDuration(TimeSpan slotDuration, List<CourtScheduleCreate> schedules)
        {
            foreach (var schedule in schedules)
            {
                // Check if the duration of a schedule day divisible by the slotDuration
                var openTime = DateTimeHelper.ConvertTimeString(schedule.OpenTime);
                var closeTime = DateTimeHelper.ConvertTimeString(schedule.CloseTime);
                var duration = closeTime - openTime;

                if (TimeSpan.Compare(slotDuration, (TimeSpan)duration!) > 0)
                {
                    throw new BadRequestException($"The total duration of schedule day {schedule.DayOfWeek.ToString()} ({DateTimeHelper.FormatTime(duration)}) cannot be shorter than the court's slot duration ({DateTimeHelper.FormatTime(slotDuration)})!");
                }

                long durationTicks = duration!.Value.Ticks;
                long slotDurationTicks = slotDuration.Ticks;

                bool isDivisible = durationTicks % slotDurationTicks == 0;
                if (!isDivisible)
                {
                    throw new BadRequestException($"The duration of schedule day {schedule.DayOfWeek.ToString()} ({schedule.OpenTime} - {schedule.CloseTime}) is not divisible by the court's slot duration ({DateTimeHelper.FormatTime(slotDuration)})!");
                }
            }
        }

        private void CheckExistingCourtSchedules(List<Schedule> courtSchedules, List<CourtScheduleCreate> newSchedules)
        {
            if (courtSchedules == null || courtSchedules.Count == 0)
            {
                return;
            }

            foreach (var schedule in courtSchedules)
            {
                if (newSchedules.Any(ns => ns.DayOfWeek == schedule.DayOfWeek))
                {
                    throw new BadRequestException($"Failed to add new schedules. The court already have schedule for day {schedule.DayOfWeek}.");
                }
            }
        }

        private TimeSpan AssignSlotDuration(SlotType slotType)
        {
            switch (slotType)
            {
                case SlotType.OneHour:
                    return new TimeSpan(1, 0, 0);
                case SlotType.OneHourHalf:
                    return new TimeSpan(1, 30, 0);
                case SlotType.TwoHour:
                    return new TimeSpan(2, 0, 0);
                case SlotType.TwoHourHalf:
                    return new TimeSpan(2, 30, 0);
                case SlotType.ThreeHour:
                    return new TimeSpan(3, 0, 0);
                default:
                    throw new BadRequestException("Invalid slot type! Failed to generate slot duration.");
            }
        }

        #endregion

        #region CourtEmployees

        public async Task<CourtDetail?> AddCourtEmployees(Guid id, AddCourtEmployeeRequest request)
        {
            await CheckUserCourtEditingPermission(id);

            // Check if court already has employee registered in new employee list
            var courtEmployees = await unitOfWork.EmployeeRepository.GetCourtEmployees(id);
            CheckExistingCourtEmployees(courtEmployees!, request.CourtEmployees);

            // Validate if new employees exist in the system
            await ValidateCourtNewEmployees(request.CourtEmployees);

            var newEmployees = GenerateCourtNewEmployees(id, request.CourtEmployees);
            try
            {
                await unitOfWork.BeginTransactionAsync();
                await unitOfWork.EmployeeRepository.AddManyAsync(newEmployees);
                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();
                var updatedCourt = await unitOfWork.CourtRepository.GetCourtFullDetail(id);
                return mapper.Map<CourtDetail>(updatedCourt);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"An unexpected error occurred when trying to add new court employees: {ex.Message}");
            }
        }

        private void CheckExistingCourtEmployees(List<Employee> courtEmployees, List<CourtEmployeeRequest> newEmployees)
        {
            if (courtEmployees == null || courtEmployees.Count == 0)
            {
                return;
            }

            foreach (var employee in courtEmployees)
            {
                if (newEmployees.Any(ne => ne.UserId == employee.UserId.ToString()))
                {
                    throw new BadRequestException($"Failed to add new employees. The court already registered user with ID {employee.UserId.ToString()} as employee.");
                }
            }
        }

        private async Task ValidateCourtNewEmployees(List<CourtEmployeeRequest> newEmployees)
        {
            if (newEmployees == null || newEmployees.Count <= 0)
            {
                throw new BadRequestException("Failed to add new court employees. The new employees list does not contains any employee!");
            }
            foreach (var employee in newEmployees)
            {
                var user = await unitOfWork.UserRepository.GetByIdAsync(Guid.Parse(employee.UserId));
                if (user == null)
                {
                    throw new NotFoundException($"Failed to add new court employees. Cannot found information of employee with user ID: {employee.UserId}");
                }

                if (user.Status != UserStatus.Active)
                {
                    throw new NotFoundException($"Failed to add new court employees. Employee with user ID: {employee.UserId} account status is NOT ACTIVE. Only user with active account can be registered as court employee!");
                }

                if (user.Role == UserRole.None || user.Role == UserRole.SystemAdmin || user.Role == UserRole.Customer)
                {
                    throw new NotFoundException($"Failed to add new court employees. Employee with user ID: {employee.UserId} is not registered as court staff or manager. Only staff or manager can be court employee!");
                }
            }
        }

        private List<Employee> GenerateCourtNewEmployees(Guid courtId, List<CourtEmployeeRequest> newEmployees)
        {
            if (newEmployees == null || newEmployees.Count <= 0)
            {
                throw new BadRequestException("Failed to add new court employees. The new employees list does not contains any employee!");
            }
            List<Employee> employees = new List<Employee>();

            foreach (var newEmployee in newEmployees)
            {
                Employee employee = new Employee()
                {
                    CourtId = courtId,
                    UserId = Guid.Parse(newEmployee.UserId),
                    Role = newEmployee.Role,
                    Status = EmployeeStatus.Active,
                };
                employees.Add(employee);
            }
            return employees;
        }

        #endregion

        #region CourtPaymentMethods

        public async Task<CourtDetail?> AddCourtPaymentMethods(Guid id, PaymentMethodCreateRequest request)
        {
            await CheckUserCourtEditingPermission(id);

            // Check for existing payment method
            var paymentMethods = await unitOfWork.PaymentMethodRepository.GetPaymentMethods(id);
            CheckExistingPaymentMethods(paymentMethods!, request.PaymentMethods);

            var newPaymentMethods = GeneratePaymentMethods(id, request.PaymentMethods);

            try
            {
                await unitOfWork.BeginTransactionAsync();
                await unitOfWork.PaymentMethodRepository.AddManyAsync(newPaymentMethods);
                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();
                var updatedCourt = await unitOfWork.CourtRepository.GetCourtFullDetail(id);
                return mapper.Map<CourtDetail>(updatedCourt);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"An unexpected error occurred when trying to add new court payment methods: {ex.Message}");
            }
        }

        private void CheckExistingPaymentMethods(List<PaymentMethod> currentPaymentMethods, List<PaymentMethodCreate> newPaymentMethods)
        {
            if (currentPaymentMethods == null || currentPaymentMethods.Count == 0)
            {
                return;
            }

            foreach (var method in currentPaymentMethods)
            {
                if (newPaymentMethods.Any(ne => ne.Type == method.MethodType && ne.Account == method.Account))
                {
                    throw new BadRequestException($"Failed to add new payment method. The court already registered payment method with Type: {method.MethodType.ToString()} and Account: {method.Account.ToString()}.");
                }
            }
        }

        private List<PaymentMethod> GeneratePaymentMethods(Guid courtId, List<PaymentMethodCreate> newPaymentMethods)
        {
            if (newPaymentMethods == null || newPaymentMethods.Count == 0)
            {
                throw new BadRequestException("Failed to add new court payment method. The new payment methods list does not contains any method!");
            }

            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            foreach (var method in newPaymentMethods)
            {
                PaymentMethod paymentMethod = new PaymentMethod()
                {
                    MethodType = method.Type,
                    Account = method.Account,
                    Status = PaymentMethodStatus.Active,
                    CourtId = courtId,
                };
                paymentMethods.Add(paymentMethod);
            }

            return paymentMethods;
        }

        #endregion

        #region CourtBookingMethods

        public async Task<CourtDetail?> AddCourtBookingMethods(Guid id, BookingMethodCreateRequest request)
        {
            await CheckUserCourtEditingPermission(id);

            // Check for existing booking method
            var bookingMethod = await unitOfWork.BookingMethodRepository.GetBookingMethods(id);
            CheckExistingBookingMethods(bookingMethod!, request.BookingMethods);

            var newBookingMethods = GenerateBookingMethods(id, request.BookingMethods);

            try
            {
                await unitOfWork.BeginTransactionAsync();
                await unitOfWork.BookingMethodRepository.AddManyAsync(newBookingMethods);
                await unitOfWork.SaveChangeAsync();
                await unitOfWork.CommitAsync();
                var updatedCourt = await unitOfWork.CourtRepository.GetCourtFullDetail(id);
                return mapper.Map<CourtDetail>(updatedCourt);
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw new Exception($"An unexpected error occurred when trying to add new court booking methods: {ex.Message}");
            }
        }

        private void CheckExistingBookingMethods(List<BookingMethod> currentBookingMethods, List<BookingMethodCreate> newBookingMethods)
        {
            if (currentBookingMethods == null || currentBookingMethods.Count == 0)
            {
                return;
            }

            foreach (var method in currentBookingMethods)
            {
                if (newBookingMethods.Any(nm => nm.Type == method.MethodType))
                {
                    throw new BadRequestException($"Failed to add new booking method. The court already registered booking method with Type: {method.MethodType.ToString()}.");
                }
            }
        }

        private List<BookingMethod> GenerateBookingMethods(Guid courtId, List<BookingMethodCreate> newBookingMethods)
        {
            if (newBookingMethods == null || newBookingMethods.Count == 0)
            {
                throw new BadRequestException("Failed to add new court booking method. The new booking methods list does not contains any method!");
            }
            List<BookingMethod> bookingMethods = new List<BookingMethod>();

            foreach (var method in newBookingMethods)
            {
                BookingMethod bookingMethod = new BookingMethod()
                {
                    MethodType = method.Type,
                    PricePerSlot = method.PricePerSlot,
                    TimePerSlot = method.TimePerSlot,
                    Status = BookingMethodStatus.Active,
                    CourtId = courtId,
                };
                bookingMethods.Add(bookingMethod);
            }

            return bookingMethods;
        }

        #endregion

        #region UserPermission

        private async Task CheckUserCourtEditingPermission(Guid courtId)
        {
            // Verify request sender account status
            jwtService.CheckActiveAccountStatus();

            Guid currentUserId = jwtService.GetCurrentUserId();

            var court = await unitOfWork.CourtRepository.GetByIdAsync(courtId);

            if (court == null)
            {
                throw new BadRequestException($"Cannot find badminton court with id: {courtId}");
            }

            await CanUserEditCourt(currentUserId, court);
        }

        private async Task CanUserEditCourt(Guid currentUserId, Court? court)
        {
            // If user is court creator then user can edit
            bool canEditCourt = court!.CreatorId == currentUserId;

            // If user is not court creator, check if user is court manager
            if (!canEditCourt)
            {
                var managersList = await unitOfWork.CourtRepository.GetCourtManagers(court.Id);
                foreach (var manager in managersList)
                {
                    if (manager.UserId == currentUserId)
                    {
                        canEditCourt = true;
                    }
                }
            }

            // If user is not court creator or manager, check if user is system admin
            if (!canEditCourt)
            {
                var currentUser = await unitOfWork.UserRepository.GetByIdAsync(currentUserId);
                if (currentUser != null && currentUser.Role == UserRole.SystemAdmin)
                {
                    canEditCourt = true;
                }
            }

            // If all conditions not satisfied, user is unauthorized to edit court
            if (!canEditCourt)
            {
                throw new UnauthorizedException("Cannot edit court information. User is not court creator or manager!");
            }
        }

        #endregion
    }
}
