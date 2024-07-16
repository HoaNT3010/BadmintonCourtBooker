using Application.ResponseDTOs.MoMo;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IMoMoService
    {
        MoMoCreatePaymentResponse? CreateMoMoPaymentForBookingTransaction(Transaction transaction);
        MoMoCreatePaymentResponse? CreateMoMoPaymentForRechargeTransaction(Transaction transaction);
    }
}
