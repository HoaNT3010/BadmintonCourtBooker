using Application.ResponseDTOs.Transaction;

namespace Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionSummary> GetPersonalFullTransaction(Guid transactionId);
    }
}
