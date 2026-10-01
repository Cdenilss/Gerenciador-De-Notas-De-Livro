using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class DeleteUsuarioHandler : IRequestHandler<DeleteUsuarioCommand,ResultViewModel>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUsuarioHandler( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.Usuario.GetById(request.Id);

        if (usuario is null)
        {
            return ResultViewModel<Guid>.Error("Usuario não encontrado");
        }
        
        usuario.SetDeleted();
        await _unitOfWork.Usuario.Update(usuario);
        await _unitOfWork.CompleteAsync();
        return  ResultViewModel.Success();
    }
}