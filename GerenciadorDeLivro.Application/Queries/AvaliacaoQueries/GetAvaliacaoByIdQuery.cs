using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.AvaliacaoViewModels;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.AvaliacaoQueries;

public class GetAvaliacaoByIdQuery : IRequest<ResultViewModel<AvaliacoesViewModel>>
{
    public GetAvaliacaoByIdQuery(Guid id)
    {
        Id = id;
    }


    public Guid Id { get; private set; }
    
}