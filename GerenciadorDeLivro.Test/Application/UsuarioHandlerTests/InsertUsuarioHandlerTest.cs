using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Application.Services;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Test.Faker.CommandsFakes;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.UsuarioHandlerTests;

public class InsertUsuarioHandlerTest
{
    [Fact]
    public async Task Handle_WhenCommandIsValid_ShouldHashAndPersistUser()
    {
        // Arrange
        const string senhaHash = "senha-com-hash";
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var authServices = Substitute.For<IAuthServices>();
        var command = InsertUsuarioFaker.CreateFakerCommand();

        unitOfWork.Usuario.Returns(usuarioRepository);
        usuarioRepository.Add(Arg.Any<Usuario>()).Returns(Task.FromResult(Guid.NewGuid()));
        unitOfWork.CompleteAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(1));
        authServices.ComputeHash(command.Senha).Returns(senhaHash);

        var handler = new InsertUsuarioHandler(authServices, unitOfWork);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeEmpty();
        authServices.Received(1).ComputeHash(command.Senha);
        await usuarioRepository.Received(1).Add(Arg.Is<Usuario>(usuario =>
            usuario!.NomeCompleto == command.Nome &&
            usuario.Email == command.Email &&
            usuario.Senha == senhaHash));
        await unitOfWork.Received(1).CompleteAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenRepositoryFails_ShouldPropagateExceptionAndNotCommit()
    {
        // Arrange
        const string errorMessage = "Erro ao inserir usuario";
        const string senhaHash = "senha-com-hash";
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var authServices = Substitute.For<IAuthServices>();
        var command = InsertUsuarioFaker.CreateFakerCommand();

        unitOfWork.Usuario.Returns(usuarioRepository);
        usuarioRepository.Add(Arg.Any<Usuario>())
            .Returns(Task.FromException<Guid>(new InvalidOperationException(errorMessage)));
        authServices.ComputeHash(command.Senha).Returns(senhaHash);

        var handler = new InsertUsuarioHandler(authServices, unitOfWork);
        Func<Task> act = () => handler.Handle(command, CancellationToken.None);

        // Act / Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage(errorMessage);

        authServices.Received(1).ComputeHash(command.Senha);
        await usuarioRepository.Received(1).Add(Arg.Any<Usuario>());
        await unitOfWork.DidNotReceive().CompleteAsync(Arg.Any<CancellationToken>());
    }
}
