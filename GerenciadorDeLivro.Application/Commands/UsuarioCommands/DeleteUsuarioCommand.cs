using GerenciadorDeLivro.Application.Models.Results;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class DeleteUsuarioCommand: IRequest<ResultViewModel>
{
    public DeleteUsuarioCommand(Guid id)
    {
       Id= id;
    }

    public Guid Id { get; set; }
}