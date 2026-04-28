using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class PutUsuarioHandler : IRequestHandler<PutUsuarioCommand, ResultViewModel>
{
  
    private readonly IUsuarioRepository _usuarioRepository;

    public PutUsuarioHandler( IUsuarioRepository usuarioRepository)
    {
        
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ResultViewModel> Handle(PutUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetById(request.UserId);
        if (usuario is null)
        {
            return ResultViewModel.Error("Usuario não encontrado");
        }
        usuario.Update(request.Nome, request.Email);
        await _usuarioRepository.Update(usuario);
        return ResultViewModel.Success();
    }
}