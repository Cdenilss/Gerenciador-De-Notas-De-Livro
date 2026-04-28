using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.LivroQueries;

public class GetAllHandler : IRequestHandler<GetAllQuery,ResultViewModel<List<LivrosItemViewModel>>>
{
    private readonly ILivroRepository _repository;

    public GetAllHandler( ILivroRepository repository)
    {
        _repository = repository;
       
    }

    public async Task<ResultViewModel<List<LivrosItemViewModel>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var livros = await _repository.GetAll();

        if (!livros.Any())
        {
            return ResultViewModel<List<LivrosItemViewModel>>.Error("Não há livros cadastrados");
        }
        var model= livros.Select(LivrosItemViewModel.FromEntity).ToList();

        return ResultViewModel<List<LivrosItemViewModel>>.Success(model);
    }
}