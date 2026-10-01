using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class PutUsuarioHandler : IRequestHandler<PutUsuarioCommand, ResultViewModel>
{
    private readonly IUnitOfWork _unitOfWork;

    public PutUsuarioHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel> Handle(PutUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.Usuario.GetById(request.UserId);
        if (usuario is null)
        {
            return ResultViewModel.Error("Usuario não encontrado");
        }

        usuario.Update(request.Nome, request.Email);
        await _unitOfWork.Usuario.Update(usuario);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return ResultViewModel.Success();
    }
}
