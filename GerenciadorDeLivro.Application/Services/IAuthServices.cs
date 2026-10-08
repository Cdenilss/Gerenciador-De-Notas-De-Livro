namespace GerenciadorDeLivro.Application.Services;

public interface IAuthServices
{
    
    string ComputeHash(string password);
    string GenerateToken(string email, string role);
    string GenerateRefreshToken(string email);
}