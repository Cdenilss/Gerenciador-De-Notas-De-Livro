using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class DeleteLivroHandler : IRequestHandler<DeleteLivroCommand, ResultViewModel>
{
   private readonly ILivroRepository _repository;

    public DeleteLivroHandler(ILivroRepository repository)
    {
        _repository = repository;
        
    }

    public async Task<ResultViewModel> Handle(DeleteLivroCommand request, CancellationToken cancellationToken)
    {
        var livro = await _repository.GetById(request.Id);
        
        if (livro is null)
        {
           return  ResultViewModel.Error("Livro não encontrado");
        }
        
     livro.SetDeleted();
      await _repository.Update(livro);
      await _repository.CommitAsync();
      return ResultViewModel.Success();

    }
}
