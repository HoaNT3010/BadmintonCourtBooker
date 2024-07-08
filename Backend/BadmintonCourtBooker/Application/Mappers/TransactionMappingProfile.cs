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
        }
    }
}
