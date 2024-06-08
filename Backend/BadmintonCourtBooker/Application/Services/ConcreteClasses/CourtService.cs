using Application.ErrorHandlers;
using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.UnitOfWork;

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
