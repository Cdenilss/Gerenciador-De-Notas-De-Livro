using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Test.Faker.CommandsFakes;
using MediatR;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.UsuarioHandlerTests;

public class ValidateInsertUsuarioCommandBehaviorTest
{
    [Fact]
    public async Task Handle_WhenEmailAlreadyExists_ShouldReturnFailureAndNotCallNextHandler()
    {
        // Arrange
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var command = InsertUsuarioFaker.CreateFakerCommand();
        var next = Substitute.For<RequestHandlerDelegate<ResultViewModel<Guid>>>();

        usuarioRepository.EmailExiste(command.Email).Returns(true);

        var behavior = new ValidateInsertUsuarioCommandBehavior(usuarioRepository);

        // Act
        var result = await behavior.Handle(command, next, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Esse email já existe");
        result.Data.Should().BeEmpty();
        await usuarioRepository.Received(1).EmailExiste(command.Email);
        await next.DidNotReceive().Invoke();
    }
}
