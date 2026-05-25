using System.Data;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Enums;
using GerenciadorDeLivro.Test.Faker;

namespace GerenciadorDeLivro.Test.Core;

public class UsuarioTest
{
    [Fact]
    public void VelhoUsuario_UpdateCall_DeveAlterarUsuario()
    {
        //avarage -> Estado que inicia
        var user = UsuarioFaker.CreateFakeUser();
        //act => açao efeituada
        user.Update("Joelma","teste2@gmail.com");
        //assert => Resultado Esperado
        
        Assert.Equal("Joelma",user.NomeCompleto);
        Assert.Equal("teste2@gmail.com", user.Email);
        Assert.NotNull(user);
        Assert.NotEqual("Carlos", user.NomeCompleto);
    }

    
}