using GerenciadorDeLivro.Core.Enums;

namespace GerenciadorDeLivro.Core.Entities;

public class Livro : BaseEntity
{
    protected Livro() 
    {
        
    }

    public Livro( string titulo, string descricao, string isbn, string autor, string editora, GeneroEnum genero, int anoDePublicacao, int quantidadeDePaginas, byte[]? capaLivro) : 
        base()
    {
        Titulo = titulo;
        Descricao = descricao;
        ISBN = isbn;
        Autor = autor;
        Editora = editora;
        Genero = genero;
        AnoDePublicacao = anoDePublicacao;
        QuantidadeDePaginas = quantidadeDePaginas;
        CapaLivro = capaLivro;
        
    }

   
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string ISBN { get; private set; }
    public string Autor { get; private set; }
    public string Editora { get; private set; }
    public GeneroEnum Genero  { get; private set; }
    public int AnoDePublicacao { get; private set; }
    public int QuantidadeDePaginas { get; private set; }
    public decimal NotaMedia { get; private set; }
    public byte[]? CapaLivro { get; private set; }
    public List<Avaliacao> AvaliacoesLivro { get; private set; } = [];
    
    public void AtualizarNotaMedia()
    {
        NotaMedia = AvaliacoesLivro.Count == 0
            ? 0
            : AvaliacoesLivro.Select(a => a.Nota).Average();
    }
}
