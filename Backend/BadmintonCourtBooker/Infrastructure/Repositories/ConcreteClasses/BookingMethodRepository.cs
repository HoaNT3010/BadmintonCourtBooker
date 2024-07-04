using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class BookingMethodRepository : BaseRepository<BookingMethod>, IBookingMethodRepository
    {
        public BookingMethodRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<BookingMethod>?> GetBookingMethods(Guid courtId)
        {
            return await dbSet.Where(e => e.CourtId == courtId).ToListAsync();
        }
    }
}
