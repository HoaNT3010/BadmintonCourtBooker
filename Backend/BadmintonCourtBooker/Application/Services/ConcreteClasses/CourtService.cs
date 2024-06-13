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

        public async Task<CourtDetail?> AddCourtSchedule(Guid id, CourtScheduleCreateRequest createRequest)
        {
            // Verify request sender account status
            jwtService.CheckActiveAccountStatus();

            Guid currentUserId = jwtService.GetCurrentUserId();

            var court = await unitOfWork.CourtRepository.GetByIdAsync(id);

            if (court == null)
            {
                throw new BadRequestException($"Cannot find badminton court with id: {id}");
            }

            await CanUserEditCourt(currentUserId, court);

            // Check if court already has a schedule for any new schedule
            var courtSchedules = await unitOfWork.ScheduleRepository.GetCourtSchedules(court.Id);
            CheckExistingCourtSchedules(courtSchedules, createRequest.Schedules);

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

        private void ValidateSchedulesDuration(TimeSpan slotDuration, List<CourtScheduleCreate> schedules)
        {
            foreach (var schedule in schedules)
            {
                // Check if the duration of a schedule day divisible by the slotDuration
                var openTime = DateTimeHelper.ConvertTimeString(schedule.OpenTime);
                var closeTime = DateTimeHelper.ConvertTimeString(schedule.CloseTime);
                var duration = closeTime - openTime;

                if (TimeSpan.Compare(slotDuration, (TimeSpan)duration) > 0)
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
    }
}
