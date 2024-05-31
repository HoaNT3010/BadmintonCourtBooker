using Application.Services.Interfaces;
using Application.Utilities.OptionsSetup;
using Domain.Entities;
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

        public JwtService(IOptions<JwtOptions> options)
        {
            jwtOptions = options.Value;
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
    }
}
