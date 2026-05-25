using FluentValidation;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;

namespace GerenciadorDeLivro.Application.Validators.UsuarioValidators;

public class InsertUsuarioValidator : AbstractValidator<InsertUsuarioCommand>
{
    public InsertUsuarioValidator()
    {
        RuleFor(usuario => usuario.Nome)
            .NotEmpty()
            .WithMessage("o Nome precisa ser fornecido")
            .MaximumLength(100)
            .WithMessage("o Nome precisa ter no máximo 100 caracteres");

        RuleFor(usuario => usuario.Email)
            .NotEmpty()
            .WithMessage("o Email precisa ser fornecido")
            .EmailAddress()
            .WithMessage("o Email fornecido não é válido")
            .MaximumLength(254)
            .WithMessage("o Email precisa ter no máximo 254 caracteres");
        
    }
}
