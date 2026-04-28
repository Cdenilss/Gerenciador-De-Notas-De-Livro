using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class ValidateInsertLivroCommandBehavior : IPipelineBehavior<InsertLivroCommand,ResultViewModel<Guid>>
{
    private readonly GerenciadorDbContext _context;

    public ValidateInsertLivroCommandBehavior(GerenciadorDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertLivroCommand request, RequestHandlerDelegate<ResultViewModel<Guid>> next, CancellationToken cancellationToken)
    {
        
        return await next();
        
    }
}