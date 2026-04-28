using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.AvaliacaoViewModels;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.AvaliacaoQueries;

public class GetAvalicaoByIdHandler : IRequestHandler<GetAvalicaoByIdQuery, ResultViewModel<AvaliacoesViewModel>>
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;

    public GetAvalicaoByIdHandler(IAvaliacaoRepository avaliacaoRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
    }

    public async Task<ResultViewModel<AvaliacoesViewModel>> Handle(GetAvalicaoByIdQuery request, CancellationToken cancellationToken)
    {
        var avalicao = await _avaliacaoRepository.GetDetailsById(request.Id);

        if (avalicao == null)
        {
            return ResultViewModel<AvaliacoesViewModel>.Error("Avalição Não Encontrada");
        }
        var model = AvaliacoesViewModel.FromEntity(avalicao);

        return ResultViewModel<AvaliacoesViewModel>.Success(model);
    }
}