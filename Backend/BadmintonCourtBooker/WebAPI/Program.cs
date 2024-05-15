using Infrastructure;
using Serilog;
using WebAPI;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddApiServices();
builder.Services.AddInfrastructureServices(connectionString!);

// Configure SeriLog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Using SeriLog
app.UseSerilogRequestLogging();

// Cors policy
app.UseCors(WebAPI.DependencyInjection.CorsPublicPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// Global exception middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
