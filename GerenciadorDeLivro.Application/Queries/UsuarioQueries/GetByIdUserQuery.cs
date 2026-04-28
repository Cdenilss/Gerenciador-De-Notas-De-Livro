using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.User;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.UsuarioQueries;

public class GetByIdUserQuery : IRequest<ResultViewModel<UsuarioViewModel>>
{
    public GetByIdUserQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}