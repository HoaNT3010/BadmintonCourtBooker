using Application.RequestDTOs.Auth;
using Application.ResponseDTOs.Auth;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> UserLogin(UserLoginRequest loginRequest);
    }
}
