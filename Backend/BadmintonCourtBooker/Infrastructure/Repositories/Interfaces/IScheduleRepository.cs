using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        Task<List<Schedule>?> GetCourtSchedules(Guid courtId);
    }
}
