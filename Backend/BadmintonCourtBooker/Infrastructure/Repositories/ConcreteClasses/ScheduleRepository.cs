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

        public async Task<Schedule?> GetScheduleToday(int today, Guid courtId)
        {
            var sche = await context.Schedules.Where(x => x.CourtId.Equals(courtId)).ToListAsync();
            var scheToday = sche.FirstOrDefault(x => x.DayOfWeek.Equals((DayOfWeek)today));
            return scheToday;
        }
    }
}
