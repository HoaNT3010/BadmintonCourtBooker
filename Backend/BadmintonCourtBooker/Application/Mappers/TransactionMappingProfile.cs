using Application.ResponseDTOs.Booking;
using Application.ResponseDTOs.Transaction;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            CreateMap<TransactionDetail, BookingTransactionDetail>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()));

            CreateMap<Transaction, BookingTransaction>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.PaymentMethod, opt => opt.MapFrom(s => s.PaymentMethod.ToString()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateTime(s.CreatedDate)));

            CreateMap<TransactionDetail, TransactionDetailSummary>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString()));

            CreateMap<User, TransactionCreator>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()));

            CreateMap<Transaction, TransactionSummary>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.PaymentMethod, opt => opt.MapFrom(s => s.PaymentMethod.ToString()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTimeHelper.FormatDateTime(s.CreatedDate)))
                .ForMember(d => d.Account, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.Account) ? "N/A" : s.Account))
                .ForMember(d => d.TransactionCode, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.TransactionCode) ? "N/A" : s.TransactionCode));
        }
    }
}
