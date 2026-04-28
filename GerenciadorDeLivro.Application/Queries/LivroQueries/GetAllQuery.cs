using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel;
using GerenciadorDeLivro.Core.Entities;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.LivroQueries;

public class GetAllQuery : IRequest<ResultViewModel<List<LivrosItemViewModel>>>
{
    
}