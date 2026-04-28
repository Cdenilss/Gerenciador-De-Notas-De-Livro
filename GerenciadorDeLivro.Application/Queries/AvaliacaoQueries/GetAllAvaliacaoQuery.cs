using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.AvaliacaoViewModels;
using GerenciadorDeLivro.Core.Entities;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.AvaliacaoQueries;

public class GetAllAvaliacaoQuery : IRequest<ResultViewModel<List<AvaliacoesViewModel>>>
{
    
}