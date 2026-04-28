using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class DeleteUsuarioHandler : IRequestHandler<DeleteUsuarioCommand,ResultViewModel>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public DeleteUsuarioHandler( IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ResultViewModel> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetById(request.Id);

        if (usuario is null)
        {
            return ResultViewModel<Guid>.Error("Usuario não encontrado");
        }
        
        usuario.SetDeleted();
        await _usuarioRepository.Update(usuario);

        
        return  ResultViewModel.Success();
    }
}