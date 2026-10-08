using GerenciadorDeLivro.Application.Models.Results;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class PasswordRecoveryCommand : IRequest<ResultViewModel>
{
    public PasswordRecoveryCommand(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
    
}
    
   