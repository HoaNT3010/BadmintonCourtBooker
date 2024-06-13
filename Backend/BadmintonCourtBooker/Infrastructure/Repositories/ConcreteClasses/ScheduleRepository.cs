using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Schedule>?> GetCourtSchedules(Guid courtId)
        {
            return await context.Schedules.Where(s => s.CourtId == courtId).ToListAsync();
        }
    }
}
