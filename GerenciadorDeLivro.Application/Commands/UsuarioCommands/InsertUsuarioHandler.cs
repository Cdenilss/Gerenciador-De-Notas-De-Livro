using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class InsertUsuarioHandler : IRequestHandler<InsertUsuarioCommand, ResultViewModel<Guid>>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public InsertUsuarioHandler( IUsuarioRepository usuarioRepository)
    {
        
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertUsuarioCommand request, CancellationToken cancellationToken)
    {
        var user= request.ToEntity();
        
        await _usuarioRepository.Add(user);
        
        return ResultViewModel<Guid>.Success(user.Id);
    }
}