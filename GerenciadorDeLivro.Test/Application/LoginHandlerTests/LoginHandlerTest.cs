using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.LoginCommands;
using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.LoginHandlerTests;

public class LoginHandlerTest
{
    private const string InvalidCredentialsMessage = "Email ou senha inválidos.";

    [Fact]
    public async Task Handle_WhenCredentialsAreValid_ShouldReturnGeneratedToken()
    {
        // Arrange
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var authService = Substitute.For<IAuthServices>();
        var command = new LoginCommand("ana@exemplo.com", "senha-valida");
        var usuario = new Usuario("Ana Silva", command.Email, "senha-com-hash");

        usuarioRepository.GetByEmail(command.Email).Returns(usuario);
        authService.ComputeHash(command.Senha).Returns("senha-com-hash");
        authService.GenerateToken(usuario.Email, usuario.Role).Returns("jwt-gerado");

        var handler = new LoginHandler(usuarioRepository, authService);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Token.Should().Be("jwt-gerado");

        await usuarioRepository.Received(1).GetByEmail(command.Email);
        authService.Received(1).ComputeHash(command.Senha);
        authService.Received(1).GenerateToken(usuario.Email, usuario.Role);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldReturnGenericFailureAndNotGenerateToken()
    {
        // Arrange
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var authService = Substitute.For<IAuthServices>();
        var command = new LoginCommand("ausente@exemplo.com", "senha-valida");

        usuarioRepository.GetByEmail(command.Email).Returns((Usuario?)null);
        authService.ComputeHash(command.Senha).Returns("senha-com-hash");

        var handler = new LoginHandler(usuarioRepository, authService);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(InvalidCredentialsMessage);
        result.Data.Should().BeNull();
        authService.DidNotReceive().GenerateToken(Arg.Any<string>(), Arg.Any<string>());
    }

    [Fact]
    public async Task Handle_WhenPasswordDoesNotMatch_ShouldReturnGenericFailureAndNotGenerateToken()
    {
        // Arrange
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var authService = Substitute.For<IAuthServices>();
        var command = new LoginCommand("ana@exemplo.com", "senha-incorreta");
        var usuario = new Usuario("Ana Silva", command.Email, "senha-armazenada");

        usuarioRepository.GetByEmail(command.Email).Returns(usuario);
        authService.ComputeHash(command.Senha).Returns("senha-com-hash");

        var handler = new LoginHandler(usuarioRepository, authService);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(InvalidCredentialsMessage);
        result.Data.Should().BeNull();
        authService.DidNotReceive().GenerateToken(Arg.Any<string>(), Arg.Any<string>());
    }
}
