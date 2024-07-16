using Application.ErrorHandlers;
using Application.ResponseDTOs.CourtStaff;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<bool> CourtCheckin(Guid id, string phone)
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
        public async Task<(List<StatsCourtResponse>,List<BookingViewBySlot>)> ViewStatsOfCourt(Guid id)
        {
            List<Slot> slot = new List<Slot>();
            List<Booking> booking = new List<Booking>();
            await CheckUserIsEmpoloyee(id);
            //Get staff infor to check id;
            var staff = await unitOfWork.EmployeeRepository.GetEmployeeByUserID(id);
            //Get court in courtid of a staff
            var court = await unitOfWork.CourtRepository.GetByIdAsync(staff.CourtId);
            //Get today
            var whatTime = DateTime.Now.TimeOfDay;
            var toDay = (int)(DateTime.Today.DayOfWeek);
            //Get schdule of this court
            //var schedule = await unitOfWork.ScheduleRepository.GetCourtSchedules(court.Id);
            var schedule = await unitOfWork.ScheduleRepository.GetScheduleToday(toDay, court.Id);
            //Get schedule of today 
            if (schedule == null) throw new BadRequestException("Court today does not open!");

            else
            {
                if (schedule.OpenTime > whatTime || whatTime > schedule.CloseTime) throw new BadRequestException("Court was closed!");
                else
                {
                    //Get slot of Court 
                    var slots = await unitOfWork.SlotRepository.GetSlotByScheduleId(schedule.Id);
                    foreach (var slotitem in slots)
                    {
                        //Only get slot active
                        if (slotitem.Available == true)
                        {
                            var today = DateTime.Now.Date;
                            slot.Add(slotitem);
                            var book = await unitOfWork.BookingRepository.GetBookingInSlotToday(today, slotitem.Id);
                            if(book != null)
                            {
                                foreach(var bookitem in book)
                                {
                                    booking.Add(bookitem);
                                }
                            }
                        }
                    }
                }
            }
            if (slot.IsNullOrEmpty()) throw new BadRequestException("Don't have any shift for you today!");
            var slotlist = mapper.Map<List<StatsCourtResponse>>(slot);
            var bookings = mapper.Map<List<BookingViewBySlot>>(booking);
            return (slotlist, bookings);
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
            //if manager
            if (!isEmp)
            {
                isEmp = roleUser.Equals("Manager") ? true : false;
            }
            if (!isEmp)
            {
                throw new UnauthorizedException("Cannot do this action. Only Employees!");
            }
            // If all conditions not satisfied, user is unauthorized to edit court
        }

        #endregion
    }
}
