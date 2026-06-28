using Application.Common.Interfaces;
using Domain.Entites;
using Infrastructure.JwtService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.TokenService
{
    public class JwtService(IOptions<JwtOptions> options) : ITokenService
    {
        private readonly JwtOptions jwtOpt = options.Value;
        public string GenerateToken(User user)
        {
            var claims = new List<Claim> 
            { 
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Email, user.Email)
            };

            var jwt = new JwtSecurityToken(
                issuer: jwtOpt.Issuer,
                audience: jwtOpt.Audience,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(jwtOpt.TokenLifetimeInMinutes)),
                claims: claims,
                signingCredentials: 
                    new SigningCredentials(jwtOpt.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
