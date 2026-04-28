using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Application.Models.InputModel;

public class CreateUserInputModel
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public Usuario ToEntity()
        => new(Nome, Email);

}