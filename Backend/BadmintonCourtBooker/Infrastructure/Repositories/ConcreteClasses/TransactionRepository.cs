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

        public async Task<Transaction?> GetCustomerFullTransaction(Guid customerId, int transactionDetailId)
        {
            return await dbSet.Include(t => t.Creator)
                .Include(t => t.TransactionDetails)
                .FirstOrDefaultAsync(t => t.CreatorId == customerId && t.TransactionDetails.Any(td => td.Id == transactionDetailId));
        }

        public async Task<bool> IsAnyPendingRechargeTransaction(Guid customerId)
        {
            return await dbSet.AnyAsync(t => t.CreatorId == customerId &&
            t.TransactionDetails.Any(td => td.Type == Domain.Enums.TransactionDetailType.BookingTimeRecharge) &&
            t.Status == Domain.Enums.TransactionStatus.Pending);
        }
    }
}
