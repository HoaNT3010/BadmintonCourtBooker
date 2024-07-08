using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<Booking> GetBookingByCustomerIdAsync(Guid id);
        Task<List<Booking>?> GetBookings(DateTime rentDate, int slotId);
    }
}
