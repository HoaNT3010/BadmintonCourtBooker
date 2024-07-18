using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        public async Task<bool> IsAnySuccessBookings(DateTime rentDate, int slotId)
        {
            return await dbSet.AnyAsync(b => b.RentDate == rentDate &&
                b.SlotId == slotId &&
                (b.Status == Domain.Enums.BookingStatus.Success));
        }

        public async Task<Booking?> GetBookingByTransactionDetail(int transactionDetailId)
        {
            return await dbSet.FirstOrDefaultAsync(b => b.TransactionDetailId == transactionDetailId);
        }

        public async Task<List<Booking>?> GetPendingBookings(DateTime rentDate, int slotId)
        {
            return await dbSet.Where(b => b.RentDate == rentDate &&
                b.SlotId == slotId &&
                (b.Status == Domain.Enums.BookingStatus.Pending))
                .ToListAsync();
        }

        public async Task<List<Booking>?> GetBookingInSlotToday(DateTime today, int slotid)
        {
            return await dbSet.Where(b => b.RentDate == today &&
            b.SlotId == slotid &&
            (b.Status == Domain.Enums.BookingStatus.Success))
            .Include(x => x.Customer)
            .ToListAsync();
        }
    }
}
