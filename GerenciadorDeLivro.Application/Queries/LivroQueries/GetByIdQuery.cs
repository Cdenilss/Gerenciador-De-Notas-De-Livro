using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.LivroQueries;

public class GetByIdQuery : IRequest<ResultViewModel<LivrosViewModel>>
{
    public Guid Id { get; set; }
    public GetByIdQuery(Guid id)
    {
        Id = id;
    }
    
}