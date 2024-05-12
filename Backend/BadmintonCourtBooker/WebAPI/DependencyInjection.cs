using WebAPI.OptionsSetup.Authentication;
using WebAPI.OptionsSetup.Jwt;
using WebAPI.OptionsSetup.Swagger;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public const string CorsPublicPolicy = "public_policy";

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPublicPolicy,
                    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Controller
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();

            // Swagger
            services.AddSwaggerGen();
            services.ConfigureOptions<SwaggerOptionsSetup>();

            // Jwt and authentication
            services.AddAuthentication().AddJwtBearer();
            services.ConfigureOptions<AuthenticationOptionsSetup>();
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            return services;
        }
    }
}
