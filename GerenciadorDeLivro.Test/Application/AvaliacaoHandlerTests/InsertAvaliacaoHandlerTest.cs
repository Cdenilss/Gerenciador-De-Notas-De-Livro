using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Test.Faker;
using GerenciadorDeLivro.Test.Faker.CommandsFakes;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.AvaliacaoHandlerTests;

public class InsertAvaliacaoHandlerTest
{
    [Fact]
    public async Task Handle_WhenLivroAndUsuarioExist_ShouldInsertAvaliacaoAndReturnSuccess()
    {
        // Arrange
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var livroRepository = Substitute.For<ILivroRepository>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var livro = LivroFaker.CreateLivroFaker();
        var command = InsertAvaliacaoFaker.CreateFakerCommand();

        unitOfWork.Livro.Returns(livroRepository);
        unitOfWork.Usuario.Returns(usuarioRepository);
        livroRepository.GetDetailsById(command.IdLivro).Returns(livro);
        usuarioRepository.Exist(command.IdUser).Returns(true);
        livroRepository.InsertAvaliacao(Arg.Any<Avaliacao>()).Returns(Task.CompletedTask);
        livroRepository.Update(livro).Returns(Task.CompletedTask);
        unitOfWork.CompleteAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(1));

        var handler = new InsertAvaliacaoHandler(unitOfWork);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeEmpty();
        livro.AvaliacoesLivro.Should().ContainSingle()
            .Which.Should().Match<Avaliacao>(avaliacao =>
                avaliacao.Nota == command.Nota &&
                avaliacao.Descricao == command.Descricao &&
                avaliacao.IdUser == command.IdUser &&
                avaliacao.IdLivro == command.IdLivro &&
                avaliacao.DataInicioLeitura == command.DataInicioLeitura &&
                avaliacao.DataFimLeitura == command.DataFimLeitura);
        livro.NotaMedia.Should().Be(command.Nota);

        await livroRepository.Received(1).InsertAvaliacao(Arg.Is<Avaliacao>(avaliacao =>
            avaliacao!.Nota == command.Nota &&
            avaliacao.IdUser == command.IdUser &&
            avaliacao.IdLivro == command.IdLivro));
        await livroRepository.Received(1).Update(livro);
        await unitOfWork.Received(1).CompleteAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenInsertAvaliacaoFails_ShouldPropagateExceptionAndNotCommit()
    {
        // Arrange
        const string errorMessage = "Erro ao inserir avaliação";
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var livroRepository = Substitute.For<ILivroRepository>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var livro = LivroFaker.CreateLivroFaker();
        var command = InsertAvaliacaoFaker.CreateFakerCommand();

        unitOfWork.Livro.Returns(livroRepository);
        unitOfWork.Usuario.Returns(usuarioRepository);
        livroRepository.GetDetailsById(command.IdLivro).Returns(livro);
        usuarioRepository.Exist(command.IdUser).Returns(true);
        livroRepository.InsertAvaliacao(Arg.Any<Avaliacao>())
            .Returns(Task.FromException(new InvalidOperationException(errorMessage)));

        var handler = new InsertAvaliacaoHandler(unitOfWork);
        Func<Task> act = () => handler.Handle(command, CancellationToken.None);

        // Act / Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage(errorMessage);

        await livroRepository.Received(1).InsertAvaliacao(Arg.Any<Avaliacao>());
        await livroRepository.DidNotReceive().Update(Arg.Any<Livro>());
        await unitOfWork.DidNotReceive().CompleteAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenLivroDoesNotExist_ShouldReturnFailureAndNotPersist()
    {
        // Arrange
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var livroRepository = Substitute.For<ILivroRepository>();
        var usuarioRepository = Substitute.For<IUsuarioRepository>();
        var command = InsertAvaliacaoFaker.CreateFakerCommand();

        unitOfWork.Livro.Returns(livroRepository);
        unitOfWork.Usuario.Returns(usuarioRepository);
        livroRepository.GetDetailsById(command.IdLivro).Returns((Livro?)null);

        var handler = new InsertAvaliacaoHandler(unitOfWork);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Livro Não Encontrado");
        result.Data.Should().BeEmpty();

        await usuarioRepository.DidNotReceive().Exist(Arg.Any<Guid>());
        await livroRepository.DidNotReceive().InsertAvaliacao(Arg.Any<Avaliacao>());
        await livroRepository.DidNotReceive().Update(Arg.Any<Livro>());
        await unitOfWork.DidNotReceive().CompleteAsync(Arg.Any<CancellationToken>());
    }
}
