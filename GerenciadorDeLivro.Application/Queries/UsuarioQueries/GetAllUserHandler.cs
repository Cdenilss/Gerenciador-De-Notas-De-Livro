using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel.User;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Queries.UsuarioQueries;

public class GetAllUser : IRequestHandler<GetAllUserQuery,ResultViewModel<List<UsuarioViewModel>>>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public GetAllUser( IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    public async Task<ResultViewModel<List<UsuarioViewModel>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _usuarioRepository.GetAll();
        
        if (usuarios is null)
        {
            return ResultViewModel<List<UsuarioViewModel>>.Error("Lista Vazia");
            
        }
        var model = usuarios.Select(UsuarioViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<UsuarioViewModel>>.Success(model);

    }
}