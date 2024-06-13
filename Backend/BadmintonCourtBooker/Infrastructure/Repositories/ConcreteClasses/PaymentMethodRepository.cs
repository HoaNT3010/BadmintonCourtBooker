using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
