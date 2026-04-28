using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Entities;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class PutUsuarioCommand: IRequest<ResultViewModel>
{ 
    
    public Guid UserId {get;set;}
    public string Nome { get; set; }
    public string Email { get; set; }

  
}