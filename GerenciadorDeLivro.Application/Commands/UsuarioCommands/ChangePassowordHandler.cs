using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Repository;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class ChangePassowordHandler: IRequestHandler<ChangePassowordCommand, ResultViewModel>
{
    private readonly IAuthServices _authServices;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;

    public ChangePassowordHandler(IAuthServices authServices, IUnitOfWork unitOfWork, IMemoryCache cache)
    {
        _authServices = authServices;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<ResultViewModel> Handle(ChangePassowordCommand request, CancellationToken cancellationToken)
    {
        var cacheKey= $"RecoveryCode:{request.Email}";

        if (!_cache.TryGetValue(cacheKey, out string? code) || code != request.Code)
        {
            return ResultViewModel.Error("Código invalido");
            
        }
        
        var user= await _unitOfWork.Usuario.GetByEmail(request.Email);

        if (user is null)
        {
            _cache.Remove(cacheKey);
            return ResultViewModel.Error("Código inválido");
        }

        var hash = _authServices.ComputeHash(request.NewPassoword);
        user.UpdatePassword(hash);
        await _unitOfWork.CompleteAsync();
        _cache.Remove(cacheKey);
        return ResultViewModel.Success();
    }
}
