using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using Moq;

namespace GerenciadorDeLivro.Test.Application.UsuarioHandlerTests;

public class DeleteUsuarioHandlerTest
{
    [Fact]
    public async Task DeleteUsuario_DeleteHandler_IsSuccess()
    {
        const string ID = "3f8d2c1a-7b4e-4a9f-9c6d-2e1f8a5b0c3d";
        var id= Guid.Parse(ID);

        var usuario = new Usuario("Carlos Teste", "Email@teste.com", "assasadsada");
        var repository = Mock.Of<IUnitOfWork>
        (r => r.Usuario.GetById(It.IsAny<Guid>()) == Task.FromResult(usuario)
              && r.Usuario.Update(It.IsAny<Usuario>()) == Task.CompletedTask);
        var command = new DeleteUsuarioCommand(Guid.Parse(ID));
        var handler = new DeleteUsuarioHandler(repository);
        var result = await handler.Handle(command, CancellationToken.None);
        Assert.True(result.IsSuccess);
        Mock.Get(repository).Verify(r=>r.Usuario.GetById(It.IsAny<Guid>()), Times.Once);
        Mock.Get(repository).Verify(r=>r.Usuario.Update(It.IsAny<Usuario>()), Times.Once);
    }
    
    [Fact]
    public async Task DeleteUsuario_DeleteHandler_IsError()
    {
        const string ID = "3f8d2c1a-7b4e-4a9f-9c6d-2e1f8a5b0c3d";
        var id= Guid.Parse(ID);

        var usuario = new Usuario("Carlos Teste", "Email@teste.com", "assasadsada");
        var repository = Mock.Of<IUnitOfWork>
        (r => r.Usuario.GetById(It.IsAny<Guid>()) == Task.FromResult((Usuario?) null)
              && r.Usuario.Update(It.IsAny<Usuario>()) == Task.CompletedTask);
        var command = new DeleteUsuarioCommand(Guid.Parse(ID));
        var handler = new DeleteUsuarioHandler(repository);
        var result = await handler.Handle(command, CancellationToken.None);
        
        Assert.False(result.IsSuccess);
        Mock.Get(repository).Verify(r=>r.Usuario.GetById(id), Times.Once);
        Mock.Get(repository).Verify(r=>r.Usuario.Update(It.IsAny<Usuario>()), Times.Never);
    }
    
    
}