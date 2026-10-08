using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LoginCommands;

public class LoginCommand : IRequest<ResultViewModel<LoginResponseViewModel>>
{
    public LoginCommand(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }

    public string Email { get; }
    public string Senha { get; }
}
