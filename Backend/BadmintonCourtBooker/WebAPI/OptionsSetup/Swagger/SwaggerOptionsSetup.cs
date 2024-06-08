using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace WebAPI.OptionsSetup.Swagger
{
    public class SwaggerOptionsSetup : IConfigureOptions<SwaggerGenOptions>
    {
        // Constants for Swagger API configuration
        const string SwaggerApiTitle = "Badminton Court Booker API";
        const string SwaggerApiVersion = "v1";
        const string SwaggerApiDescription = "ASP .NET core API for project Badminton Court Booker";
        // Constants related to JWT Bearer Token authentication
        const string BearerTokenName = "JWT Bearer Token";
        const string BearerTokenDescription = "Please enter a valid JWT Bearer Token";
        const string BearerTokenSchemeName = "Authorization";
        const string BearerTokenSchemeFormat = "JWT";
        const string BearerTokenScheme = "Bearer";

        public void Configure(SwaggerGenOptions options)
        {
            // Configure Swagger documentation for the specified API version
            options.SwaggerDoc(SwaggerApiVersion,
            new OpenApiInfo
            {
                Title = SwaggerApiTitle,
                Version = SwaggerApiVersion,
                Description = SwaggerApiDescription,
            });

            // Include XML comments from the assembly for API documentation
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            // Add security definition for JWT Bearer Token
            options.AddSecurityDefinition(BearerTokenName,
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = BearerTokenDescription,
                Name = BearerTokenSchemeName,
                Type = SecuritySchemeType.Http,
                BearerFormat = BearerTokenSchemeFormat,
                Scheme = BearerTokenScheme,
            });

            // Specify security requirement for endpoints using the Bearer token
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = BearerTokenName,
                        }
                    },
                    new string[] {}
                }
            });
        }
    }
}
