using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.LivrosCommands;
using GerenciadorDeLivro.Application.Validators.LivroValidators;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Enums;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Test.Faker.CommandsFakes;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.LivrosHandlerTests;

public class InsertLivroHandlerTest
{
 
    //caminho feliz
    [Fact]
    public async Task InsertLivroDataOk_Insert_IsSuccess()
    {
        //arrage
        const string ID = "3f8d2c1a-7b4e-4a9f-9c6d-2e1f8a5b0c3d";
        var id= Guid.Parse(ID);
        var repository = Substitute.For<ILivroRepository>();
        repository.Add(Arg.Any<Livro>()).Returns(Task.FromResult(id));


        var command = InsertLivroFaker.CreateFakerCommand();
        
        var handler = new InsertLivroHandler(repository);
        //act
        var result = await handler.Handle(command, CancellationToken.None);
        //assert
       result.IsSuccess.Should().BeTrue();
       
        await repository.Received(1).Add(Arg.Any<Livro>());
    }
    [Fact]
    public async Task InsertLivro_WhenRepositoryFails_ShouldThrowException()
    {
        // arrange
        var repository = Substitute.For<ILivroRepository>();

        repository
            .Add(Arg.Any<Livro>())
            .Returns<Task<Guid>>(_ => throw new Exception("Erro ao criar livro"));

        var command = InsertLivroFaker.CreateFakerCommand();
        var handler = new InsertLivroHandler(repository);

        // act
        var act = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Erro ao criar livro");

        await repository.Received(1).Add(Arg.Any<Livro>());
        await repository.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    





}
