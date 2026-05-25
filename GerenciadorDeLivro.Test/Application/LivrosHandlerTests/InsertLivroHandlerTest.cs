using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.LivrosCommands;
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

    // [Fact]
    // public async Task InsertLivroBadRequest_Insert_IsErrorMessage()
    // {
    //     const string ERROR = "Testando voce aqui e agora";
    //     const string ID = "3f8d2c1a-7b4e-4a9f-9c6d-2e1f8a5b0c3d";
    //     var id= Guid.Parse(ID);
    //     var repository = Substitute.For<ILivroRepository>();
    //     repository.Add(Arg.Any<Livro>()).Returns(Task.FromResult(id));
    //     
    //     var command= new InsertLivroCommand()
    //     {
    //         Titulo =" " ,
    //         Descricao ="Testando voce aqui e agora",
    //         ISBN ="12892818",
    //         Autor ="Denil Teste brasil",
    //         Editora ="Test 34",
    //         Genero = GeneroEnum.Fantasia,
    //         AnoDePublicacao=2000,
    //         QuantidadeDePaginas =340,
    //         NotaMedia = 0,
    //         CapaLivro= null
    //     };
    //     
    //     var handler = new InsertLivroHandler(repository);
    //     var result = await handler.Handle(command, CancellationToken.None);
    //     // Assert.False(result.IsSuccess);
    //     
    //     // Assert.Equal("Há um erro de requisiçao",result.Message );
    //     
    //     await repository.DidNotReceiveWithAnyArgs().Add(Arg.Any<Livro>());
    //     
    //     
    // }
    
}