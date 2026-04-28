namespace GerenciadorDeLivro.Core.Entities;

public class Avaliacao : BaseEntity
{
    protected Avaliacao() 
    {
        
    }

    public Avaliacao(decimal nota, string? descricao, Guid idUser, Guid idLivro, DateTime dataInicioLeitura, DateTime dataFimLeitura): base()
    {
        Nota = nota;
        Descricao = descricao;
        IdUser = idUser;
        IdLivro = idLivro;
        DataInicioLeitura = dataInicioLeitura;
        DataFimLeitura = dataFimLeitura;
        
    }
    
    public decimal Nota { get; private set; }
    public string? Descricao { get; private set; }
    public Usuario Usuario { get; private set; }
    public Guid IdUser { get; private set; }
    public Livro Livro { get; private set; }
    public Guid IdLivro { get; private set; }
    public DateTime DataInicioLeitura { get; private set; }
    public DateTime DataFimLeitura { get; private set; }
    
    public void ValidarData()
    {
        throw new NotImplementedException();
    }

    public void ValidarNota()
    {
        throw new NotImplementedException();
    }
}