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

        public async Task<List<Booking>> GetBookingInSlotToday(DateTime today, int slotId)
        {
            return await dbSet.Include(c => c.Customer)
                .Include(b => b.Slot)
                .Where(d => d.RentDate.Equals(today) && d.SlotId == slotId).ToListAsync();
        }
    }
}
