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

        public async Task<Booking?> GetBookingByCustomerIdAsync(Guid id)
        {
            return await context.Bookings
                .FirstOrDefaultAsync(x => x.CustomerId.Equals(id));
        }
    }
}
