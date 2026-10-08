using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GerenciadorDeLivro.Infrastructure.Auth;

public class TokenHelpers
{
    public static TokenValidationParameters GetTokenValidateParameters(IConfiguration configuration)
    {
        var tokenKey= Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]?? throw new InvalidOperationException("SecretKey não configurada"));

        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
        };
    }
}