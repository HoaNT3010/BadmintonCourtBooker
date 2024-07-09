using Application.Services.ConcreteClasses;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            // AutoMapper
            services.AddAutoMapper(assembly);

            // FluentValidation
            services.AddValidatorsFromAssembly(assembly);
            services.AddFluentValidationAutoValidation();

            // Services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourtService, CourtService>();
            services.AddScoped<ICourtStaffService, CourtStaffService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
