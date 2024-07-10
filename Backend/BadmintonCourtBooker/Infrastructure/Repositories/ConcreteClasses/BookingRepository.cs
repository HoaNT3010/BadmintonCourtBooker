using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Booking>?> GetPendingAndSuccessBookings(DateTime rentDate, int slotId)
        {
            return await dbSet.Include(b => b.Slot)
                .Where(b => b.RentDate == rentDate &&
                b.SlotId == slotId &&
                (b.Status == Domain.Enums.BookingStatus.Pending || b.Status == Domain.Enums.BookingStatus.Success))
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByCustomerIdAsync(Guid id)
        {
            return await context.Bookings
                .FirstOrDefaultAsync(x => x.CustomerId.Equals(id));
        }

        public async Task<Booking?> GetFullCustomerBooking(Guid customerId, Guid bookingId)
        {
            return await dbSet.Include(b => b.Court)
                .Include(b => b.Slot)
                .Include(b => b.BookingMethod)
                .Include(b => b.Customer)
                .Include(b => b.TransactionDetail)
                .FirstOrDefaultAsync(b => b.CustomerId == customerId && b.Id == bookingId);
        }
    }
}
