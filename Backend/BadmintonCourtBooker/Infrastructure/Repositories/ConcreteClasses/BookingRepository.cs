using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
