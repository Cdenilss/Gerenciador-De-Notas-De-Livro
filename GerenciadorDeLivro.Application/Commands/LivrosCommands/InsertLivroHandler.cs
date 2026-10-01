using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class InsertLivroHandler: IRequestHandler<InsertLivroCommand, ResultViewModel<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    public InsertLivroHandler( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertLivroCommand request, CancellationToken cancellationToken)
    {
        var livro= request.ToEntity();
        
         await _unitOfWork.Livro.Add(livro);
         await _unitOfWork.CompleteAsync();
        return  ResultViewModel<Guid>.Success(livro.Id);
    }
}