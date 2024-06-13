using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<List<Employee>?> GetCourtEmployees(Guid courtId);
    }
}
