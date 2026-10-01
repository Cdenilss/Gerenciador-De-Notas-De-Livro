using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class DeleteLivroHandler : IRequestHandler<DeleteLivroCommand, ResultViewModel>
{
   private readonly IUnitOfWork _unitOfWork;

    public DeleteLivroHandler( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> Handle(DeleteLivroCommand request, CancellationToken cancellationToken)
    {
        var livro = await _unitOfWork.Livro.GetById(request.Id);
        
        if (livro is null)
        {
           return  ResultViewModel.Error("Livro não encontrado");
        }
        
     livro.SetDeleted();
      await _unitOfWork.Livro.Update(livro);
      await _unitOfWork.CompleteAsync();
      return ResultViewModel.Success();

    }
}
