using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<Transaction?> GetCustomerFullTransaction(Guid customerId, Guid transactionId);
    }
}
