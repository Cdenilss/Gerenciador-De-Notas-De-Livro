using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel;
using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Repository;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LoginCommands;

public class LoginHandler : IRequestHandler<LoginCommand, ResultViewModel<LoginResponseViewModel>>
{
    private const string InvalidCredentialsMessage = "Email ou senha inválidos.";

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAuthServices _authService;

    public LoginHandler(IUsuarioRepository usuarioRepository, IAuthServices authService)
    {
        _usuarioRepository = usuarioRepository;
        _authService = authService;
    }

    public async Task<ResultViewModel<LoginResponseViewModel>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByEmail(request.Email);
        var senhaHash = _authService.ComputeHash(request.Senha);

        if (usuario is null || usuario.Senha != senhaHash)
        {
            return ResultViewModel<LoginResponseViewModel>.Error(InvalidCredentialsMessage);
        }

        var token = _authService.GenerateToken(usuario.Email, usuario.Role);
        return ResultViewModel<LoginResponseViewModel>.Success(new LoginResponseViewModel(token));
    }
}
