using Serilog;
using WebAPI;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiServices();

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
app.UseCors(DependencyInjection.CORS_PUBLIC_POLICY_NAME);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// Global exception middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
