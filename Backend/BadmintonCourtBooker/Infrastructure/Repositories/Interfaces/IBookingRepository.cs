using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<Booking> GetBookingByCustomerIdAsync(Guid id);
        Task<List<Booking>?> GetPendingAndSuccessBookings(DateTime rentDate, int slotId);
        Task<Booking?> GetFullCustomerBooking(Guid customerId, Guid bookingId);
    }
}
