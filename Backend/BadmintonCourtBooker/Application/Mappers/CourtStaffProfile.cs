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
        }
    }
}
