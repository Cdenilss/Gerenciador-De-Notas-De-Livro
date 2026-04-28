using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.AvaliacaoViewModels;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.AvaliacaoQueries;

public class GetAvalicaoByIdQuery : IRequest<ResultViewModel<AvaliacoesViewModel>>
{
    public GetAvalicaoByIdQuery(Guid id)
    {
        Id = id;
    }


    public Guid Id { get; private set; }
    
}