using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class InsertUsuarioHandler : IRequestHandler<InsertUsuarioCommand, ResultViewModel<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public InsertUsuarioHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertUsuarioCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();
        await _unitOfWork.Usuario.Add(user);
        await _unitOfWork.CompleteAsync(cancellationToken);
        return ResultViewModel<Guid>.Success(user.Id);
    }
}
