using FluentAssertions;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Test.Faker.CommandsFakes;
using Moq;

namespace GerenciadorDeLivro.Test.Application.UsuarioHandlerTests;

public class InsertUsuarioHandlerTest
{
    [Fact]
    public async Task InsertUsuario_Insert_Sucesso()
    {
        //arrage
        
        const string ID = "3f8d2c1a-7b4e-4a9f-9c6d-2e1f8a5b0c3d";
        var id= Guid.Parse(ID);
       var repository= Mock.Of<IUsuarioRepository>(r=>r.Add(It.IsAny<Usuario>())==Task.FromResult(id));

       var command = InsertUsuarioFaker.CreateFakerCommand();
        
        var handler = new InsertUsuarioHandler(repository);
        //act
        var result = await handler.Handle(command, CancellationToken .None);
        //assert
        
        result.IsSuccess.Should().BeTrue();
        Mock.Get(repository).Verify(x => x.Add(It.IsAny<Usuario>()), Times.Once);
        
    }

    [Fact]
    public async Task InsertUsuario_Insert_Excecao()
    {
        //arrage
        const string ERROR_MESSAGE = "Erro ao inserir usuario";
        var repository = new Mock<IUsuarioRepository>();
        repository
            .Setup(x => x.Add(It.IsAny<Usuario>()))
            .ThrowsAsync(new Exception(ERROR_MESSAGE));

        var command = new InsertUsuarioCommand()
        {
            Nome = "Nome 1",
            Email = "Email@gmail.com"
        };

        var handler = new InsertUsuarioHandler(repository.Object);

        //act
        var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

        //assert
        Assert.Equal(ERROR_MESSAGE, exception.Message);
        
        exception.Message.Should().Be(ERROR_MESSAGE);

        repository.Verify(x => x.Add(It.IsAny<Usuario>()), Times.Once);
    }
    
}
