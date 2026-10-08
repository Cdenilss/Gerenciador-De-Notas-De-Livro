using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeLivro.Application.Models.InputModel;

public class LoginRequestInputModel
{

    public required string Email { get; set; }
    
    public required string Senha { get; set; }
}