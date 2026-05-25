using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Enums;

namespace GerenciadorDeLivro.Test.Core;

public class LivroTests
{
    [Fact]
    public void NotaMedia_UpdateCall_DeveAlterarMedia()
    {
        var livro = Faker.LivroFaker.CreateLivroFaker();
        
        var avaliacao= new Avaliacao(3.2m,"bacana",Guid.NewGuid(), Guid.NewGuid(), DateTime.Now,DateTime.Now);
        var avaliacao2= new Avaliacao(4.2m,"bacana",Guid.NewGuid(), Guid.NewGuid(), DateTime.Now,DateTime.Now);
        
        livro.AvaliacoesLivro.Add(avaliacao);
        livro.AvaliacoesLivro.Add(avaliacao2);
        livro.AtualizarNotaMedia();
        
        Assert.Equal(3.7m, livro.NotaMedia);
        Assert.Equal(2, livro.AvaliacoesLivro.Count);
        

     
        
        
    }

}