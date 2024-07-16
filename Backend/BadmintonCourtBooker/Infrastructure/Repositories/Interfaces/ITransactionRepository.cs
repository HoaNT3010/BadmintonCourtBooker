using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<Transaction?> GetCustomerFullTransaction(Guid customerId, Guid transactionId);
        Task<Transaction?> GetCustomerFullTransaction(Guid customerId, int transactionDetailId);
        Task<bool> IsAnyPendingRechargeTransaction(Guid customerId);
    }
}
