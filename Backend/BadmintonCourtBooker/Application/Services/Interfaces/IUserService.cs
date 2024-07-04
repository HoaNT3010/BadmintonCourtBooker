using Application.RequestDTOs.Auth;
using Application.ResponseDTOs.Auth;
using Application.ResponseDTOs;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> UserLogin(UserLoginRequest loginRequest);
        Task<CustomerRegisterResponse?> RegisterCustomer(CustomerRegisterRequest registerRequest);
        Task<User?> GetCurrentUserProfileById();
        Task<User?> GetUserDetailById(Guid requestId);
        Task<PagedResult<ListCustomerResponse>> GetListUser(int pageNumber, int pageSize);
        Task<bool> BanUserById(Guid requestId);
        Task<PagedResult<ListCustomerResponse>> SearchByNameByEmailByPhone(SearchCustomerRequest searchCustomerRequest, int pageNumber, int pageSize);
        Task<bool> UpdateUserById(Guid userId, CustomerRegisterRequest updatedUser);
        Task<bool> UpdateCurrentUserById(CustomerRegisterRequest updatedUser);
    }
}
