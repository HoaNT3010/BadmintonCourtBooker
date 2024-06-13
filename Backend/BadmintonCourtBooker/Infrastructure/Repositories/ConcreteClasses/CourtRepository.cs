using Domain.Entities;
using Domain.Enums;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class CourtRepository : BaseRepository<Court>, ICourtRepository
    {
        public CourtRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Court?> GetCourtFullDetail(Guid id)
        {
            return await context.Courts
                .Include(c => c.Employees)
                    .ThenInclude(e => e.User)
                .Include(c => c.BookingMethods)
                .Include(c => c.PaymentMethods)
                .Include(c => c.Schedules)
                    .ThenInclude(s => s.Slots)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Employee>> GetCourtManagers(Guid id)
        {
            return await context.Employees.Where(e =>
            e.Role == EmployeeRole.Manager
            && e.Status == EmployeeStatus.Active
            && e.CourtId == id)
                .ToListAsync();
        }
    }
}
