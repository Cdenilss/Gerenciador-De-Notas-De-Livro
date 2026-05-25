using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class ValidateInsertLivroCommandBehavior : IPipelineBehavior<InsertLivroCommand,ResultViewModel<Guid>>
{
   
    private readonly ILivroRepository _repository;

    public ValidateInsertLivroCommandBehavior( ILivroRepository repository)
    {
        _repository = repository;
        
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertLivroCommand request, RequestHandlerDelegate<ResultViewModel<Guid>> next, CancellationToken cancellationToken)
    {
        var isbn=request.ISBN;
        var isbnJaExiste = await _repository.ExisteIsbnAsync(isbn, cancellationToken);
        
        if (isbnJaExiste)
        {
            return ResultViewModel<Guid>.Error("Já existe um livro cadastrado com este ISBN");
        }
        
        return await next();
    }
}
