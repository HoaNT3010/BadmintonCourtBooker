using Application.RequestDTOs.Court;
using Application.ResponseDTOs.Booking;
using Application.ResponseDTOs.Court;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Utilities.Paging;

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

            // Court details
            CreateMap<User, CourtCreatorDetail>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => $"{s.LastName} {s.FirstName}"));

            CreateMap<Employee, CourtEmployee>()
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDate(s.StartDate)))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDate(s.EndDate)))
                .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role.ToString()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User!.Email))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => $"{s.User!.LastName} {s.User.FirstName}"));

            CreateMap<Slot, SlotDetail>()
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => DateTimeHelper.FormatTime(s.StartTime)))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => DateTimeHelper.FormatTime(s.EndTime)));

            CreateMap<Schedule, ScheduleDetail>()
                .ForMember(d => d.DayOfWeek, opt => opt.MapFrom(s => s.DayOfWeek.ToString()))
                .ForMember(d => d.OpenTime, opt => opt.MapFrom(s => DateTimeHelper.FormatTime(s.OpenTime)))
                .ForMember(d => d.CloseTime, opt => opt.MapFrom(s => DateTimeHelper.FormatTime(s.CloseTime)));

            CreateMap<PaymentMethod, PaymentMethodDetail>()
                .ForMember(d => d.MethodType, opt => opt.MapFrom(s => s.MethodType.ToString()));

            CreateMap<BookingMethod, BookingMethodDetail>()
                .ForMember(d => d.MethodType, opt => opt.MapFrom(s => s.MethodType.ToString()));

            CreateMap<Court, CourtDetail>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.CourtType, opt => opt.MapFrom(s => s.CourtType.ToString()))
                .ForMember(d => d.SlotType, opt => opt.MapFrom(s => s.SlotType.ToString()))
                .ForMember(d => d.SlotDuration, opt => opt.MapFrom(s => DateTimeHelper.FormatTime(s.SlotDuration)))
                .ForMember(d => d.CourtStatus, opt => opt.MapFrom(s => s.CourtStatus.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDate(s.CreatedDate)));

            CreateMap<Court, CourtShortDetail>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.CourtType, opt => opt.MapFrom(s => s.CourtType.ToString()))
                .ForMember(d => d.SlotDuration, opt => opt.MapFrom(s => DateTimeHelper.FormatTime(s.SlotDuration)))
                .ForMember(d => d.CourtStatus, opt => opt.MapFrom(s => s.CourtStatus.ToString()));

            CreateMap<PagedList<Court>, PagedList<CourtShortDetail>>();

            CreateMap<Court, BookingCourt>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()));

            CreateMap<CourtUpdateRequest, Court>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name.ToString()))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description.ToString()))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber.ToString()))
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address.ToString()))
                .ForMember(d => d.CourtType, opt => opt.MapFrom(s => s.CourtType.ToString()));


        }
    }
}
