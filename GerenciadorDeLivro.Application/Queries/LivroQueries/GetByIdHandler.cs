using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.LivroQueries;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, ResultViewModel<LivrosViewModel>>
{
    private readonly ILivroRepository _repository;

    public GetByIdHandler( ILivroRepository repository)
    {
        _repository = repository;
       
    }

    public async Task<ResultViewModel<LivrosViewModel>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var livro = await _repository.GetDetailsById(request.Id);
        if (livro is null)
        {
            return ResultViewModel<LivrosViewModel>.Error("Livro nao encontrado");
        }
        var model= LivrosViewModel.FromEntity(livro);
        return ResultViewModel<LivrosViewModel>.Success(model);
    }
    }

