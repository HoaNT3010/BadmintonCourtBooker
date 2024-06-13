using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICourtRepository : IBaseRepository<Court>
    {
        Task<List<Employee>> GetCourtManagers(Guid id);
        Task<Court?> GetCourtFullDetail(Guid id);
    }
}
