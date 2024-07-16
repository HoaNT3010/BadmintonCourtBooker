using Application.RequestDTOs.MoMo;
using Application.RequestDTOs.Transaction;
using Application.ResponseDTOs.MoMo;
using Application.ResponseDTOs.Transaction;
using Infrastructure.Utilities.Paging;

namespace Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionSummary> GetPersonalFullTransaction(Guid transactionId);
        Task<TransactionSummary> GetPersonalFullTransactionByDetail(int transactionDetailId);
        Task<PagedList<TransactionShortSummary>?> GetPersonalTransactions(TransactionQueryRequest queryRequest);
        Task<TransactionSummary> ProcessBookingTimeTransaction(Guid transactionId);
        Task<MoMoCreatePaymentResponse> CreateMoMoPaymentForBookingTransaction(Guid transactionId);
        Task ProcessMoMoPaymentResponse(MoMoIpnRequest ipnRequest);
    }
}
