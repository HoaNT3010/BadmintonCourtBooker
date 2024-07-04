using Application.ResponseDTOs;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, CustomerRegisterResponse>()
                .ForMember(d => d.Role, s => s.MapFrom(s => s.Role.ToString()))
                .ForMember(d => d.Status, s => s.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.CreatedDate, s => s.MapFrom(s => DateTimeHelper.FormatDateTime(s.CreatedDate)));
            CreateMap<User, ListCustomerResponse>()
                .ForMember(d => d.Id, s => s.MapFrom(s => s.Id))
                .ForMember(d => d.Email, s => s.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber, s => s.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Status, s => s.MapFrom(s => s.Status))
                .ForMember(d => d.CreatedDate, s => s.MapFrom(s => DateTimeHelper.FormatDateTime(s.CreatedDate)));
        }
    }
}
