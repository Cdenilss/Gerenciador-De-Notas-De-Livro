using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class InsertLivroHandler: IRequestHandler<InsertLivroCommand, ResultViewModel<Guid>>
{
    private readonly GerenciadorDbContext _context;

    public InsertLivroHandler(GerenciadorDbContext context)
    {
        _context = context;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertLivroCommand request, CancellationToken cancellationToken)
    {
        var livro= request.ToEntity();
     
       await _context.Livros.AddAsync(livro);
        await _context.SaveChangesAsync(cancellationToken);
        return  ResultViewModel<Guid>.Success(livro.Id);
    }
}