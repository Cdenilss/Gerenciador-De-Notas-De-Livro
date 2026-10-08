namespace GerenciadorDeLivro.Application.Models.ViewModel;

public class LoginResponseViewModel
{
    public LoginResponseViewModel(string token)
    {
        Token = token;
    }

    public string Token { get; set; }
    
}