using GerenciadorDeLivro.Application.Models.Results;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class ChangePassowordCommand : IRequest<ResultViewModel>
{
    public ChangePassowordCommand(string email, string code, string newPassoword)
    {
        Email = email;
        Code = code;
        NewPassoword = newPassoword;
    }

    public string Email{get; set;}
    public string Code{get; set;}
    public string NewPassoword{get;set;}
    
}