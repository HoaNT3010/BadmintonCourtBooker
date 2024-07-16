using Application.RequestDTOs.Auth;
using Application.ResponseDTOs.Auth;
using Application.ResponseDTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> UserLogin(UserLoginRequest loginRequest);
        Task<CustomerRegisterResponse?> RegisterCustomer(CustomerRegisterRequest registerRequest);
        Task<ProfileResponse?> GetCurrentUserProfileById();
        Task<ProfileResponse?> GetUserDetailById(Guid requestId);
        Task<PagedResult<ListCustomerResponse>> GetListUser(int pageNumber, int pageSize);
        Task<bool> BanUserById(Guid requestId);
        Task<PagedResult<ListCustomerResponse>> SearchByNameByEmailByPhone(SearchCustomerRequest searchCustomerRequest, int pageNumber, int pageSize);
        Task<bool> UpdateUserById(Guid userId, CustomerUpdateRequest updatedUser);
        Task<bool> UpdateCurrentUserById(CustomerUpdateRequest updatedUser);
        Task<(bool isSuccess, string message)> UpdateRoleUserById(Guid requestId, UserRole Role);
    }
}
