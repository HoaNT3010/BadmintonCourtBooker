using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
    }
}
