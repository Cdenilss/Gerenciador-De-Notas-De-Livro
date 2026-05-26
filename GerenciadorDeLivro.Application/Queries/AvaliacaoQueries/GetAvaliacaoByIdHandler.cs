using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.AvaliacaoViewModels;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.AvaliacaoQueries;

public class GetAvaliacaoByIdHandler : IRequestHandler<GetAvaliacaoByIdQuery, ResultViewModel<AvaliacoesViewModel>>
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;

    public GetAvaliacaoByIdHandler(IAvaliacaoRepository avaliacaoRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
    }

    public async Task<ResultViewModel<AvaliacoesViewModel>> Handle(GetAvaliacaoByIdQuery request, CancellationToken cancellationToken)
    {
        var avaliacao = await _avaliacaoRepository.GetDetailsById(request.Id);

        if (avaliacao == null)
        {
            return ResultViewModel<AvaliacoesViewModel>.Error("Avaliação não encontrada");
        }
        var model = AvaliacoesViewModel.FromEntity(avaliacao);

        return ResultViewModel<AvaliacoesViewModel>.Success(model);
    }
}
