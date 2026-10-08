namespace GerenciadorDeLivro.Core.Entities;

public class Usuario: BaseEntity
{
    protected Usuario() 
    {
    }
    
    public Usuario(string nomeCompleto, string email, string senha): base()
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        Senha = senha;
        Role = "user";
    }

    public string NomeCompleto { get; private set; }
    public string Email { get; private set; }
    public List<Avaliacao> AvaliacoesUserList { get; private set; } = [];
    public string Senha { get; private set; }
    public string Role { get; private set;}



    public void Update(string nome, string email)
    {
        NomeCompleto = nome;
        Email = email;
    }
    
    public void UpdatePassword(string senha)
    {
        Senha = senha;
    }
    
}