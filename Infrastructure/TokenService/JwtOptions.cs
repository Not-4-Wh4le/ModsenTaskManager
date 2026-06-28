using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.JwtService
{
    public record JwtOptions()
    {
        public string Issuer { get; init; } = string.Empty;
        public string SecretKey { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public int TokenLifetimeInMinutes { get; init; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
            => new(Encoding.UTF8.GetBytes(SecretKey));
    }
}
