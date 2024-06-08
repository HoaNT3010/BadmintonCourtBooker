using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class CourtRepository : BaseRepository<Court>, ICourtRepository
    {
        public CourtRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
