using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GerenciadorDeLivro.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GerenciadorDeLivro.Infrastructure.Auth;

public class AuthService : IAuthServices
{
    private readonly IConfiguration _configuration;
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string ComputeHash(string senha)
    {
        using (var hash = SHA256.Create())
        {
            var senhaBytes = Encoding.UTF8.GetBytes(senha);
            var hashBytes=  hash.ComputeHash(senhaBytes);
            var builder = new StringBuilder();
            for (var i = 0; i < hashBytes.Length; i++)
                
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public string GenerateToken(string email, string role)
    {
        var jwt = _configuration.GetSection("Jwt");

        var secretKey = jwt["SecretKey"] ?? throw new InvalidOperationException("Jwt:SecretKey não configurada.");
        var issuer= jwt["Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer não configurado");
        var audience= jwt["Audience"]??  throw new InvalidOperationException("Jwt:Audience não configurado");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var claims= new List<Claim>()
        {
            
            new Claim(JwtRegisteredClaimNames.Email,
                email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role,role)
        };

        var credetials= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        
        var expires= jwt.GetValue<int>("ExpirationTimeInMinutes");

        var token = new JwtSecurityToken(
        
           issuer : issuer,
           audience: audience,
           claims: claims,
           notBefore: null,
           expires: DateTime.UtcNow.AddMinutes(expires),
           signingCredentials: credetials
           
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(string email)
    {
        throw new NotImplementedException();
    }
}