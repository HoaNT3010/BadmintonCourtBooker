using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<Booking>?> GetBookings(DateTime rentDate, int slotId);
    }
}
