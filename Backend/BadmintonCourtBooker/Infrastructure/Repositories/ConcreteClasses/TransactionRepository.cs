using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Transaction?> GetCustomerFullTransaction(Guid customerId, Guid transactionId)
        {
            return await dbSet.Include(t => t.Creator)
                .Include(t => t.TransactionDetails)
                .FirstOrDefaultAsync(t => t.CreatorId == customerId && t.Id == transactionId);
        }
    }
}
