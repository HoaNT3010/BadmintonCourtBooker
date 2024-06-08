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
    }
}
