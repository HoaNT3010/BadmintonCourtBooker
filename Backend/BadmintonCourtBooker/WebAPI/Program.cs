using WebAPI;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors policy
app.UseCors(DependencyInjection.CORS_PUBLIC_POLICY_NAME);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// Global exception middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
