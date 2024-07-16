using Application.ResponseDTOs.CourtStaff;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class CourtStaffProfile : Profile
    {
        public CourtStaffProfile()
        {
            CreateMap<Slot, StatsCourtResponse>()
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime.ToString()))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime.ToString()));
            CreateMap<Booking, BookingViewBySlot>()
                .ForMember(d => d.BookingId, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.Checkin, opt => opt.MapFrom(s => s.CheckIn.ToString()))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.Customer.FirstName.ToString() + " " + s.Customer.LastName))
                .ForMember(d => d.Start, opt => opt.MapFrom(s => s.Slot.StartTime.ToString()))
                .ForMember(d => d.End, opt => opt.MapFrom(s => s.Slot.EndTime.ToString()));
        }

    }
}
