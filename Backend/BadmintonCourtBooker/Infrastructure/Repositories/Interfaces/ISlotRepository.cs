using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ISlotRepository : IBaseRepository<Slot>
    {
        Task<List<Slot>> GetSlotByScheduleId(int scheduleId);
    }
}
