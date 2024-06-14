using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class TransactionDetailRepository : BaseRepository<TransactionDetail>, ITransactionDetailRepository
    {
        public TransactionDetailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
