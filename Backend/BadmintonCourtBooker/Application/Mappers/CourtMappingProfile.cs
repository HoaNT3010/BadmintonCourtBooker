using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Court;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class CourtMappingProfile : Profile
    {
        public CourtMappingProfile()
        {
            CreateMap<CourtCreateRequest, Court>();

            CreateMap<Court, CourtCreateResponse>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.CourtType, opt => opt.MapFrom(s => s.CourtType.ToString()))
                .ForMember(d => d.SlotType, opt => opt.MapFrom(s => s.SlotType.ToString()))
                .ForMember(d => d.SlotDuration, opt => opt.MapFrom(s => DateTimeHelper.FormatTime(s.SlotDuration)))
                .ForMember(d => d.CourtStatus, opt => opt.MapFrom(s => s.CourtStatus.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDate(s.CreatedDate)))
                .ForMember(d => d.CreatorId, opt => opt.MapFrom(s => s.CreatorId.ToString()));

            CreateMap<Employee, CourtEmployee>()
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDate(s.StartDate)))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDate(s.EndDate)))
                .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role.ToString()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
        }
    }
}
