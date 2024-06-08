using Application.ErrorHandlers;
using Application.Services.Interfaces;
using Application.Utilities.OptionsSetup;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.ConcreteClasses
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions jwtOptions;
        private readonly IHttpContextAccessor contextAccessor;

        public JwtService(IOptions<JwtOptions> options, IHttpContextAccessor contextAccessor)
        {
            jwtOptions = options.Value;
            this.contextAccessor = contextAccessor;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("Role", user.Role.ToString()),
                new Claim("Status", user.Status.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddHours(3),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }

        public string GenerateRefreshToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("Role", user.Role.ToString()),
                new Claim("Status", user.Status.ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddDays(7),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }

        public Guid GetCurrentUserId()
        {
            if (contextAccessor.HttpContext?.User.Identity is ClaimsIdentity claimsIdentity && claimsIdentity.Claims.Any())
            {
                Claim userIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Id");
                if (userIdClaim == null)
                {
                    throw new UnauthorizedException("Id claim not found in token. Please check bearer token!");
                }

                return Guid.Parse(userIdClaim.Value);
            }
            throw new UnauthorizedException("Invalid bearer token!");
        }

        public string GetCurrentUserStatus()
        {
            if (contextAccessor.HttpContext?.User.Identity is ClaimsIdentity claimsIdentity && claimsIdentity.Claims.Any())
            {
                Claim userStatusClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Status");
                if (userStatusClaim == null)
                {
                    throw new UnauthorizedException("Status claim not found in token. Please check bearer token!");
                }

                return userStatusClaim.Value;
            }
            throw new UnauthorizedException("Invalid bearer token!");
        }

        public void CheckActiveAccountStatus()
        {
            var userStatus = GetCurrentUserStatus();
            if (userStatus != UserStatus.Active.ToString())
            {
                throw new BadRequestException("User's account has not been activated. Please contact system admin.");
            }
        }
    }
}
