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
            CreateMap<Court, StatsCourtResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.ToString()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.CourtStatus.ToString()));
        }
    }
}
