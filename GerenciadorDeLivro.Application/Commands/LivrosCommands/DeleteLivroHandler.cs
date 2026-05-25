using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class DeleteLivroHandler : IRequestHandler<DeleteLivroCommand, ResultViewModel>
{
   private readonly ILivroRepository _repository;

    public DeleteLivroHandler(GerenciadorDbContext context, ILivroRepository repository)
    {
        _repository = repository;
        
    }

    public async Task<ResultViewModel> Handle(DeleteLivroCommand request, CancellationToken cancellationToken)
    {
        var livro = await _repository.GetById(request.Id);
        if (livro is null)
        {
            ResultViewModel.Error("Livro Nao encontrado");
        }
        livro.SetDeleted();
      await _repository.Update(livro);
      await _repository.CommitAsync();
      return ResultViewModel.Success();

    }
}