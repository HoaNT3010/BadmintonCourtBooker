using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>
    {
        Task<List<PaymentMethod>?> GetPaymentMethods(Guid courtId);
    }
}
