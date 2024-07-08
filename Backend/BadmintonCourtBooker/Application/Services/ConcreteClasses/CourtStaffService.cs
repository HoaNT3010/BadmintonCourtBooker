using Application.ErrorHandlers;
using Application.ResponseDTOs.CourtStaff;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ConcreteClasses
{
    public class CourtStaffService : ICourtStaffService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;
        public CourtStaffService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.jwtService = jwtService;
        }
        public async Task<bool> CourtCheckin(Guid id,string phone)
        {
            await CheckUserIsEmpoloyee(id);

            var cusID = await unitOfWork.UserRepository.GetByPhoneNumberAsync(phone);
            var getBooking = await unitOfWork.BookingRepository.GetBookingByCustomerIdAsync(cusID.Id);

            if (getBooking != null)
            {
                getBooking.CheckIn = true;
                return true;
            }
            else return false;
        }
        //View Status of court statu by employee
        public async Task<StatsCourtResponse> ViewStatsOfCourt(Guid id)
        {
            await CheckUserIsEmpoloyee(id);

            var staff = await unitOfWork.EmployeeRepository.GetEmployeeByUserID(id);

            var court = await unitOfWork.CourtRepository.GetByIdAsync(staff.CourtId);

            return mapper.Map<StatsCourtResponse>(court);
        }
        #region UserPermission

        private async Task CheckUserIsEmpoloyee(Guid courtId)
        {
            // Verify request sender account status
            jwtService.CheckActiveAccountStatus();

            Guid currentUserId = jwtService.GetCurrentUserId();

            var emp = await unitOfWork.EmployeeRepository.GetEmployeeByUserID(courtId);

            if (emp == null)
            {
                throw new BadRequestException($"Cannot active this action with id: {courtId}");
            }

            await isStaff(currentUserId, emp);
        }

        private async Task isStaff(Guid currentUserId, Employee? emp)
        {
            var getUser = await unitOfWork.EmployeeRepository.GetEmployeeByUserID(emp.UserId);

            var roleUser = getUser.Role.ToString();

            bool isEmp = roleUser.Equals("Staff") ? true : false;

            if (!isEmp)
            {
                throw new UnauthorizedException("Cannot do this action. Only Employees!");
            }
            // If all conditions not satisfied, user is unauthorized to edit court
        }

        #endregion
    }
}
