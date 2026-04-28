using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.AvaliacaoViewModels;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.AvaliacaoQueries;

public class GetAllAvaliacaoHandler : IRequestHandler<GetAllAvaliacaoQuery,ResultViewModel<List<AvaliacoesViewModel>>>
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;

    public GetAllAvaliacaoHandler(IAvaliacaoRepository avaliacaoRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
    }

    public async Task<ResultViewModel<List<AvaliacoesViewModel>>> Handle(GetAllAvaliacaoQuery request, CancellationToken cancellationToken)
    {
       var avaliacao= await _avaliacaoRepository.GetAll();
       
       if (!avaliacao.Any())
       {
           return ResultViewModel<List<AvaliacoesViewModel>>.Error("Não há livros cadastrados");
       }
        
       var model= avaliacao.Select(AvaliacoesViewModel.FromEntity).ToList();

       return ResultViewModel<List<AvaliacoesViewModel>>.Success(model);
       
    }
}