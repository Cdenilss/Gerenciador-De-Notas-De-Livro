using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Application.Models.ViewModel.User;

public class UsuarioViewModel
{
    public UsuarioViewModel(Guid id, string nomeCompleto, string email, List<Avaliacao>? avaliacoes)
    {
        Id = id;
        NomeCompleto = nomeCompleto;
        Email = email;
        Avaliacoes = avaliacoes?.Select(a=>a.Livro.Titulo).ToList();

    }
    
    
    public Guid Id { get; private set; }
    public string NomeCompleto { get; private set; }
    public string Email { get; private set; }   
    public List<string>? Avaliacoes { get; private set; }


    public static UsuarioViewModel FromEntity(Usuario entity)
    {
       
       return new UsuarioViewModel (entity.Id, entity.NomeCompleto, entity.Email,entity.AvaliacoesUserList);
    }
    

}