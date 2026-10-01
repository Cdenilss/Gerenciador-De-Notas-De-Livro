using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Test.Faker.CommandsFakes;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.UsuarioHandlerTests;

public class InsertUsuarioHandlerTest
{
    [Fact]
    public async Task Handle_WhenRepositorySucceeds_ShouldPersistUser()
    {
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var repository = Substitute.For<IUsuarioRepository>();
        var command = InsertUsuarioFaker.CreateFakerCommand();
        unitOfWork.Usuario.Returns(repository);
        repository.Add(Arg.Any<Usuario>()).Returns(Task.FromResult(Guid.NewGuid()));

        var result = await new InsertUsuarioHandler(unitOfWork).Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await repository.Received(1).Add(Arg.Any<Usuario>());
        await unitOfWork.Received(1).CompleteAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenRepositoryFails_ShouldNotCommit()
    {
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var repository = Substitute.For<IUsuarioRepository>();
        var command = InsertUsuarioFaker.CreateFakerCommand();
        unitOfWork.Usuario.Returns(repository);
        repository.Add(Arg.Any<Usuario>())
            .Returns(Task.FromException<Guid>(new InvalidOperationException("Erro ao inserir usuario")));

        Func<Task> act = () => new InsertUsuarioHandler(unitOfWork).Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Erro ao inserir usuario");
        await unitOfWork.DidNotReceive().CompleteAsync(Arg.Any<CancellationToken>());
    }
}
