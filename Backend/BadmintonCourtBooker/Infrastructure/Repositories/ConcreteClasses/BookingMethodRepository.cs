using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class BookingMethodRepository : BaseRepository<BookingMethod>, IBookingMethodRepository
    {
        public BookingMethodRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
