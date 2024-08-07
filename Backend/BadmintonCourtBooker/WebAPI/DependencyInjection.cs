﻿using System.Text.Json.Serialization;
using WebAPI.OptionsSetup.Authentication;
using WebAPI.OptionsSetup.Authorization;
using WebAPI.OptionsSetup.Jwt;
using WebAPI.OptionsSetup.MoMo;
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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
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

            // Authorization
            services.ConfigureOptions<AuthorizationOptionsSetup>();

            // MoMo
            services.ConfigureOptions<MoMoOptionsSetup>();

            // Prometheus

            return services;
        }
    }
}
