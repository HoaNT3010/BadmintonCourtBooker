using Application.RequestDTOs.Auth;
using Application.ResponseDTOs.Auth;
using Application.ResponseDTOs;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> UserLogin(UserLoginRequest loginRequest);
        Task<CustomerRegisterResponse?> RegisterCustomer(CustomerRegisterRequest registerRequest);
    }
}
