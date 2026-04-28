namespace GerenciadorDeLivro.Core.Entities;

public class Usuario: BaseEntity
{
    protected Usuario() 
    {
    }
    
    public Usuario(string nomeCompleto, string email): base()
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        
    }

    public string NomeCompleto { get; private set; }
    public string Email { get; private set; }
    public List<Avaliacao> AvaliacoesUserList { get; private set; } = [];

    public void Update(string nome, string email)
    {
        NomeCompleto = nome;
        Email = email;
    }
    
}