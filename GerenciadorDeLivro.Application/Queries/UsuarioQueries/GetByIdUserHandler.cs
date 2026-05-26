using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.User;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.UsuarioQueries;

public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery,ResultViewModel<UsuarioViewModel>>
{
 
    private readonly IUsuarioRepository _usuarioRepository;

    public GetByIdUserHandler( IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
 
    public async Task<ResultViewModel<UsuarioViewModel>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetDetailsById(request.Id);
        
        if (usuario == null)
        {
            return ResultViewModel<UsuarioViewModel>.Error("Usuário não encontrado");
        }
        var model = UsuarioViewModel.FromEntity(usuario);
        
        return ResultViewModel<UsuarioViewModel>.Success(model);
    }
}
