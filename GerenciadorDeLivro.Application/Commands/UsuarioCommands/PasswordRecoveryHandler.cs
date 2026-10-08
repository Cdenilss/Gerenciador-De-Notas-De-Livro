using System.Security.Cryptography;
using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Repository;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace GerenciadorDeLivro.Application.Commands.UsuarioCommands;

public class PasswordRecoveryHandler: IRequestHandler<PasswordRecoveryCommand, ResultViewModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailServices _emailServices;
    private readonly IMemoryCache _cache;

    
    public PasswordRecoveryHandler(IUnitOfWork unitOfWork, IEmailServices emailServices, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _emailServices = emailServices;
        _cache = cache;
    }

    public async Task<ResultViewModel> Handle(PasswordRecoveryCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Usuario.GetByEmail(request.Email);

        if (user is null)
        {
            return ResultViewModel.Success();
        }
        
        var code = RandomNumberGenerator
            .GetInt32(10000, 100000)
            .ToString();
        var sendResult = await _emailServices.SendEmailAsync(
            request.Email,
            "Código de recuperação",
            $"Seu código de recuperação é: {code}",
            cancellationToken);

        if (sendResult.IsSuccess)
        {
            var cacheKey = $"RecoveryCode:{request.Email}";
            _cache.Set(cacheKey, code, TimeSpan.FromHours(1));
        }

        return ResultViewModel.Success();

    }
    
}
