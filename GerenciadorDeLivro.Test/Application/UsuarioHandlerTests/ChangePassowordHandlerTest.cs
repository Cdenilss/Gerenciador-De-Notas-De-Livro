using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.UsuarioHandlerTests;

public class ChangePassowordHandlerTest
{
    [Fact]
    public async Task Handle_WhenCodeIsValid_ShouldPersistTheHashedPassword()
    {
        // Arrange
        const string email = "ana@exemplo.com";
        const string code = "12345";
        const string newPassword = "nova-senha";
        const string passwordHash = "senha-com-hash";
        var user = new Usuario("Ana Silva", email, "senha-antiga-com-hash");
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var authServices = Substitute.For<IAuthServices>();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var command = new ChangePassowordCommand(email, code, newPassword);

        cache.Set($"RecoveryCode:{email}", code);
        unitOfWork.Usuario.Returns(usuarioRepository);
        usuarioRepository.GetByEmail(email).Returns(user);
        authServices.ComputeHash(newPassword).Returns(passwordHash);
        unitOfWork.CompleteAsync(Arg.Any<CancellationToken>()).Returns(1);
        var handler = new ChangePassowordHandler(authServices, unitOfWork, cache);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        user.Senha.Should().Be(passwordHash);
        cache.TryGetValue($"RecoveryCode:{email}", out _).Should().BeFalse();
        authServices.Received(1).ComputeHash(newPassword);
        await unitOfWork.Received(1).CompleteAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenPersistingPasswordFails_ShouldKeepRecoveryCode()
    {
        // Arrange
        const string email = "ana@exemplo.com";
        const string code = "12345";
        var user = new Usuario("Ana Silva", email, "senha-antiga-com-hash");
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var authServices = Substitute.For<IAuthServices>();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var command = new ChangePassowordCommand(email, code, "nova-senha");

        cache.Set($"RecoveryCode:{email}", code);
        unitOfWork.Usuario.Returns(usuarioRepository);
        usuarioRepository.GetByEmail(email).Returns(user);
        authServices.ComputeHash(command.NewPassoword).Returns("senha-com-hash");
        unitOfWork.CompleteAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromException<int>(new InvalidOperationException("Falha no banco")));
        var handler = new ChangePassowordHandler(authServices, unitOfWork, cache);

        // Act
        Func<Task> act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Falha no banco");
        cache.TryGetValue<string>($"RecoveryCode:{email}", out var cachedCode).Should().BeTrue();
        cachedCode.Should().Be(code);
    }
}
