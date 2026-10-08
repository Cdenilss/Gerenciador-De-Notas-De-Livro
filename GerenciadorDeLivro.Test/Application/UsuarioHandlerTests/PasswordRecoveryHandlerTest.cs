using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.UsuarioHandlerTests;

public class PasswordRecoveryHandlerTest
{
    [Fact]
    public async Task Handle_WhenEmailDeliveryFails_ShouldNotStoreRecoveryCode()
    {
        // Arrange
        const string email = "ana@exemplo.com";
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var emailServices = Substitute.For<IEmailServices>();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var command = new PasswordRecoveryCommand(email);

        unitOfWork.Usuario.Returns(usuarioRepository);
        usuarioRepository.GetByEmail(email).Returns(new Usuario("Ana Silva", email, "senha-com-hash"));
        emailServices.SendEmailAsync(
                email,
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>())
            .Returns(EmailSendResult.Failure());
        var handler = new PasswordRecoveryHandler(unitOfWork, emailServices, cache);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        cache.TryGetValue($"RecoveryCode:{email}", out _).Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WhenEmailIsAccepted_ShouldStoreRecoveryCode()
    {
        // Arrange
        const string email = "ana@exemplo.com";
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var emailServices = Substitute.For<IEmailServices>();
        using var cache = new MemoryCache(new MemoryCacheOptions());
        var command = new PasswordRecoveryCommand(email);

        unitOfWork.Usuario.Returns(usuarioRepository);
        usuarioRepository.GetByEmail(email).Returns(new Usuario("Ana Silva", email, "senha-com-hash"));
        emailServices.SendEmailAsync(
                email,
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>())
            .Returns(EmailSendResult.Success());
        var handler = new PasswordRecoveryHandler(unitOfWork, emailServices, cache);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        cache.TryGetValue<string>($"RecoveryCode:{email}", out var code).Should().BeTrue();
        code.Should().MatchRegex("^\\d{5}$");
    }
}
