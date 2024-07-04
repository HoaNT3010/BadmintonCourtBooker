using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBookingMethodRepository : IBaseRepository<BookingMethod>
    {
        Task<List<BookingMethod>?> GetBookingMethods(Guid courtId);
    }
}
