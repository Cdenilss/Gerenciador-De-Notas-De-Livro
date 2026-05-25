using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.LivrosCommands;
using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Validators.LivroValidators;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Test.Faker.CommandsFakes;
using MediatR;
using NSubstitute;

namespace GerenciadorDeLivro.Test.Application.LivrosHandlerTests;

public class ValidatorLivroTest
{
    [Fact]

    public async Task InsertLivroValido_ShouldBeSuccess()
    {
        var validator = new InsertLivroValidator();
        var command = InsertLivroFaker.CreateFakerCommand();
        var result = validator.Validate(command); 
        
        result.IsValid.Should().BeTrue();
        

    }
    
    [Fact]
    public void InsertLivro_WhenTituloIsEmpty_ShouldBeInvalid()
    {
        var validator = new InsertLivroValidator();
        var command = InsertLivroFaker.CreateFakerCommandComTituloVazio();

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "O título precisa ser fornecido");
    }
    
    [Fact]
    public async Task InsertLivro_WhenIsbnAlreadyExists_ShouldReturnFailure()
    {
        // arrange
        var repository = Substitute.For<ILivroRepository>();

        repository
            .ExisteIsbnAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(true);

        var behavior = new ValidateInsertLivroCommandBehavior(repository);
        var command = InsertLivroFaker.CreateFakerCommand();

        var next = Substitute.For<RequestHandlerDelegate<ResultViewModel<Guid>>>();

        // act
        var result = await behavior.Handle(command, next, CancellationToken.None);

        // assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Já existe um livro cadastrado com este ISBN");

        await repository.Received(1)
            .ExisteIsbnAsync(command.ISBN, Arg.Any<CancellationToken>());

        await next.DidNotReceive().Invoke();
    }
    
    [Fact]
    public void InsertLivro_WhenTituloHasMoreThan200Characters_ShouldBeInvalid()
    {
        var validator = new InsertLivroValidator();
        var command = InsertLivroFaker.CreateFakerCommandComTituloMaiorQuePermitido();

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.ErrorMessage == "O título precisa ter no máximo 200 caracteres");
    }

    [Fact]
    public void InsertLivro_WhenTituloHasExactly200Characters_ShouldBeValid()
    {
        var validator = new InsertLivroValidator();
        var command = InsertLivroFaker.CreateFakerCommand();
        command.Titulo = new string('A', 200);

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }
}
