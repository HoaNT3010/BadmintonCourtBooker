using Infrastructure.Context;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Repositories.ConcreteClasses;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // Db context
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourtRepository, CourtRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
