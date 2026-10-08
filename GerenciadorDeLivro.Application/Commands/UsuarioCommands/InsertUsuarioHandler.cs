using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class InsertUsuarioHandler : IRequestHandler<InsertUsuarioCommand, ResultViewModel<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthServices _authServices;

    public InsertUsuarioHandler( IAuthServices authServices, IUnitOfWork unitOfWork)
    {
        _authServices = authServices;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> Handle(InsertUsuarioCommand request, CancellationToken cancellationToken)
    {
        var hash= _authServices.ComputeHash(request.Senha);
        var user= request.ToEntity(hash);
        await _unitOfWork.Usuario.Add(user);

        await _unitOfWork.CompleteAsync();
        return ResultViewModel<Guid>.Success(user.Id);
    }
}