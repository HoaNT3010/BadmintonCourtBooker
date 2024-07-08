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

        public async Task<List<Booking>?> GetBookings(DateTime rentDate, int slotId)
        {
            return await dbSet.Include(b => b.Slot)
                .Where(b => b.RentDate == rentDate && b.SlotId == slotId)
                .ToListAsync();
        }
    }
}
