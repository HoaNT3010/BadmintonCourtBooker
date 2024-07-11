using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class SlotRepository : BaseRepository<Slot>, ISlotRepository
    {
        public SlotRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Slot>> GetSlotByScheduleId(int scheduleId)
        {
            return await context.Slots.Where(x => x.ScheduleId.Equals(scheduleId)).ToListAsync();
        }
    }
}
