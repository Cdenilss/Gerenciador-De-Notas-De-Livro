using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.User;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.UsuarioQueries;

public class GetAllUserQuery : IRequest<ResultViewModel<List<UsuarioViewModel>>>
{
    
}