using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class InsertLivroHandler: IRequestHandler<InsertLivroCommand, ResultViewModel<Guid>>
{
    private readonly ILivroRepository _repository;
    
    public InsertLivroHandler( ILivroRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertLivroCommand request, CancellationToken cancellationToken)
    {
        var livro= request.ToEntity();
        
         await _repository.Add(livro);
         await _repository.CommitAsync();
        return  ResultViewModel<Guid>.Success(livro.Id);
    }
}