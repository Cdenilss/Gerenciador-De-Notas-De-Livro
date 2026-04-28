using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Application.Models.InputModel;

public class CreateUpadateUsuarioInputModel
{
    public CreateUpadateUsuarioInputModel(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public string Nome { get; set; }
    public string Email { get; set; }

    
}