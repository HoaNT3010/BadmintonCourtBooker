using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
