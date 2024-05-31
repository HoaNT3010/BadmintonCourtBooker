using Application.ErrorHandlers;
using Application.RequestDTOs.Auth;
using Application.ResponseDTOs.Auth;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace Application.Services.ConcreteClasses
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtService jwtService;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IJwtService jwtService, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.jwtService = jwtService;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
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
