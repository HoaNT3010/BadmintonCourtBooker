using Application.ErrorHandlers;
using Application.RequestDTOs.Auth;
using Application.ResponseDTOs.Auth;
using Application.ResponseDTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Data.UnitOfWork;
using Domain.Entities;
using BC = BCrypt.Net.BCrypt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;

namespace Application.Services.ConcreteClasses
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService jwtService;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.jwtService = jwtService;
            this.mapper = mapper;
        }

        public async Task<CustomerRegisterResponse?> RegisterCustomer(CustomerRegisterRequest registerRequest)
        {
            // Check if email is unique
            if (await unitOfWork.UserRepository.GetByEmailAsync(registerRequest.Email) != null)
            {
                throw new BadRequestException($"Account's email {registerRequest.Email} has been used! Please use different email to register.");
            }

            // Check if phone number is unique
            if (await unitOfWork.UserRepository.GetByPhoneNumberAsync(registerRequest.PhoneNumber) != null)
            {
                throw new BadRequestException($"Account's phone number {registerRequest.PhoneNumber} has been used! Please use different phone number to register.");
            }

            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = registerRequest.Email,
                PasswordHash = BC.EnhancedHashPassword(registerRequest.Password),
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                PhoneNumber = registerRequest.PhoneNumber,
                Role = UserRole.Customer,
                Status = UserStatus.Unverified,
            };

            try
            {
                await unitOfWork.UserRepository.AddAsync(newUser);
                int result = await unitOfWork.SaveChangeAsync();
                if (result == 0)
                {
                    throw new Exception("Unexpected error occurred when trying to add new customer!");
                }
                return mapper.Map<CustomerRegisterResponse>(newUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoginResponse> UserLogin(UserLoginRequest loginRequest)
        {
            var user = await unitOfWork.UserRepository.Login(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                throw new BadRequestException("Incorrect login credentials!");
            }

            if (user.Status == UserStatus.Suspended)
            {
                throw new UnauthorizedException("User is suspended! Please contact system admin to resolve this issue.");
            }

            return new LoginResponse()
            {
                AccessToken = jwtService.GenerateAccessToken(user),
                UserRole = user.Role.ToString(),
                UserStatus = user.Status.ToString(),
            };
        }

        public async Task<ProfileResponse?> GetCurrentUserProfileById()
        {
            // Verify request sender account status
            jwtService.CheckActiveAccountStatus();

            Guid currentUserId = jwtService.GetCurrentUserId();
            var existProfile = await unitOfWork.UserRepository.GetByIdAsync(currentUserId);
            if (existProfile == null)
            {
                throw new NotFoundException("User Not Exist");
            }
            var response = mapper.Map<ProfileResponse>(existProfile);
            return response;
        }

        public async Task<ProfileResponse?> GetUserDetailById(Guid requestId)
        {
            var existProfile = await unitOfWork.UserRepository.GetByIdAsync(requestId);
            if (existProfile == null)
            {
                throw new NotFoundException("User Not Exist");
            }
            var response = mapper.Map<ProfileResponse>(existProfile);
            return response;
        }

        public async Task<PagedResult<ListCustomerResponse>> GetListUser(int pageNumber, int pageSize)
        {
            var listProfile = unitOfWork.UserRepository.GetListUser();
            if (listProfile == null)
            {
                throw new NotFoundException("List is empty");
            }           
            var pagedUsers = await Paging.Paging.GetPagedResultAsync(listProfile, pageNumber, pageSize);
            var response = mapper.Map<List<ListCustomerResponse>>(pagedUsers.Items);
            return new PagedResult<ListCustomerResponse>
            {
                Items = response,
                TotalCount = pagedUsers.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


        public async Task<bool> BanUserById(Guid requestId)
        {
            var existProfile = await unitOfWork.UserRepository.GetByIdAsync(requestId);
            if (existProfile == null)
            {
                throw new NotFoundException("User Not Exist");               
            }
            if (existProfile.Status.Equals(UserStatus.Active)){
                existProfile.Status = UserStatus.Suspended;
            }
            else
            {
                existProfile.Status = UserStatus.Active;
            }
            try
            {
                unitOfWork.UserRepository.Update(existProfile);
                await unitOfWork.SaveChangeAsync();
            }catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            
            return true;
        }

        public async Task<PagedResult<ListCustomerResponse>> SearchByNameByEmailByPhone(SearchCustomerRequest searchCustomerRequest, int pageNumber, int pageSize)
        {
           
                var customers =  unitOfWork.UserRepository.SearchByNameByEmailByPhoneByStatus(searchCustomerRequest.FirstName, searchCustomerRequest.Email, searchCustomerRequest.PhoneNumber, searchCustomerRequest.Status);
                if (customers == null || !customers.Any())
                {
                    throw new NotFoundException("List is empty");
                }

                var pagedUsers = await Paging.Paging.GetPagedResultAsync(customers, pageNumber, pageSize);

                var response = mapper.Map<List<ListCustomerResponse>>(pagedUsers.Items);

                return new PagedResult<ListCustomerResponse>
                {
                    Items = response,
                    TotalCount = pagedUsers.TotalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }


        public async Task<bool> UpdateUserById(Guid userId, CustomerUpdateRequest updatedUser)
        {
            
            var existingUser = await unitOfWork.UserRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found");
            }
            if (!string.IsNullOrEmpty(updatedUser.Password))
            {
                updatedUser.Password = BC.EnhancedHashPassword(updatedUser.Password);
            }
            mapper.Map(updatedUser,existingUser);
            try
            {
                unitOfWork.UserRepository.Update(existingUser);

                await unitOfWork.SaveChangeAsync();
            }catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            

            return true;
        }

        public async Task<bool> UpdateCurrentUserById(CustomerUpdateRequest updatedUser)
        {
            // Verify request sender account status
            jwtService.CheckActiveAccountStatus();

            Guid currentUserId = jwtService.GetCurrentUserId();
            var existingUser = await unitOfWork.UserRepository.GetByIdAsync(currentUserId);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found");
            }
            if (!string.IsNullOrEmpty(updatedUser.Password))
            {
                updatedUser.Password = BC.EnhancedHashPassword(updatedUser.Password);
            }
            mapper.Map(updatedUser, existingUser);
            try
            {
                unitOfWork.UserRepository.Update(existingUser);

                await unitOfWork.SaveChangeAsync();
            }catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
           

            return true;
        }

        public async Task<(bool isSuccess, string message)> UpdateRoleUserById(Guid requestId, UserRole Role)
        {
            var existProfile = await unitOfWork.UserRepository.GetByIdAsync(requestId);
            if (existProfile == null)
            {
                throw new NotFoundException("User Not Exist");
            }
            if (existProfile.Role.Equals(Role))
            {
                return (false, "User role is already the same.");
            }
            existProfile.Role = Role;
            
            try
            {
                unitOfWork.UserRepository.Update(existProfile);
                await unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

            return (true, "User role updated successfully.");
        }
    }
}
