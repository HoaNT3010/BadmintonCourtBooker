using Application.RequestDTOs.Transaction;
using Application.ResponseDTOs.Transaction;
using Infrastructure.Utilities.Paging;

namespace Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionSummary> GetPersonalFullTransaction(Guid transactionId);
        Task<TransactionSummary> GetPersonalFullTransactionByDetail(int transactionDetailId);
        Task<PagedList<TransactionShortSummary>?> GetPersonalTransactions(TransactionQueryRequest queryRequest);
    }
}
