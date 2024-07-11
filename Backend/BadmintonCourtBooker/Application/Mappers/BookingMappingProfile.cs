using Application.ResponseDTOs.Booking;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Utilities.Paging;

namespace Application.Mappers
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<Booking, CreateBookingResponse>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.RentDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateWithName(s.RentDate)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateTime(s.CreatedDate)));

            CreateMap<Booking, MultipleBooking>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.RentDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateWithName(s.RentDate)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateTime(s.CreatedDate)));

            CreateMap<Booking, BookingShortDetail>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.RentDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateWithName(s.RentDate)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateTime(s.CreatedDate)))
                .ForMember(d => d.CourtName, opt => opt.MapFrom(s => s.Court != null ? s.Court.Name.ToString() : "N/A"))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.Slot != null ? DateTimeHelper.FormatTime(s.Slot.StartTime) : "N/A"))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.Slot != null ? DateTimeHelper.FormatTime(s.Slot.EndTime) : "N/A"));

            CreateMap<PagedList<Booking>, PagedList<BookingShortDetail>>();

            CreateMap<User, BookingCustomer>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()));

            CreateMap<Booking, BookingDetail>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.RentDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateWithName(s.RentDate)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateTime(s.CreatedDate)))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.Slot != null ? DateTimeHelper.FormatTime(s.Slot.StartTime) : "N/A"))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.Slot != null ? DateTimeHelper.FormatTime(s.Slot.EndTime) : "N/A"));
        }
    }
}
