using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
        Guid GetCurrentUserId();
        string GetCurrentUserStatus();
        void CheckActiveAccountStatus();
    }
}
