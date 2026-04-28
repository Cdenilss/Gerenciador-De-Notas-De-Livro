using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Entities;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class InsertUsuarioCommand : IRequest<ResultViewModel<Guid>>
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public Usuario ToEntity()
        => new(Nome, Email);
    
}