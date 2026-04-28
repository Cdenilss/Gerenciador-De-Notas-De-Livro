using GerenciadorDeLivro.Application.Models.Results;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class DeleteLivroCommand : IRequest<ResultViewModel>
{
    public DeleteLivroCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
    
}