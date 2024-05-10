using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public const string CORS_PUBLIC_POLICY_NAME = "public_policy";
        public const string SWAGGER_API_VERSION = "v1";
        public const string SWAGGER_API_DESCRIPTION = "ASP .NET core API for project Badminton Court Booker";
        public const string SWAGGER_SECURITY_DEFINITION_NAME = "JWT Bearer Token";
        public const string SWAGGER_SECURITY_DEFINITION_SCHEME_DESCRIPTION = "Please enter a valid JWT Bearer Token";
        public const string SWAGGER_SECURITY_DEFINITION_SCHEME_NAME = "Authorization";
        public const string SWAGGER_SECURITY_DEFINITION_SCHEME_FORMAT = "JWT";
        public const string SWAGGER_SECURITY_DEFINITION_SCHEME = "Bearer";

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_PUBLIC_POLICY_NAME,
                    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Controller
            services.AddControllers();

            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(SWAGGER_API_VERSION,
                    new OpenApiInfo
                    {
                        Title = "Badminton Court Booker API",
                        Version = SWAGGER_API_VERSION,
                        Description = SWAGGER_API_DESCRIPTION,
                    });
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.AddSecurityDefinition(SWAGGER_SECURITY_DEFINITION_NAME,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = SWAGGER_SECURITY_DEFINITION_SCHEME_DESCRIPTION,
                        Name = SWAGGER_SECURITY_DEFINITION_SCHEME_NAME,
                        Type = SecuritySchemeType.Http,
                        BearerFormat = SWAGGER_SECURITY_DEFINITION_SCHEME_FORMAT,
                        Scheme = SWAGGER_SECURITY_DEFINITION_SCHEME,
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = SWAGGER_SECURITY_DEFINITION_SCHEME,
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
