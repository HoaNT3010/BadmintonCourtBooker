using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<Booking?> GetBookingByCustomerIdAsync(Guid id);
        Task<List<Booking>?> GetPendingAndSuccessBookings(DateTime rentDate, int slotId);
        Task<Booking?> GetFullCustomerBooking(Guid customerId, Guid bookingId);
        Task<bool> IsAnySuccessBookings(DateTime rentDate, int slotId);
        Task<Booking?> GetBookingByTransactionDetail(int transactionDetailId);
        Task<List<Booking>?> GetPendingBookings(DateTime rentDate, int slotId);
        Task<List<Booking>?> GetBookingInSlotToday(DateTime today, int slotid);
    }
}
