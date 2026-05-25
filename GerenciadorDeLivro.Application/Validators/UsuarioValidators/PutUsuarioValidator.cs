using System.Data;
using FluentValidation;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;

namespace GerenciadorDeLivro.Application.Validators.UsuarioValidators;

public class PutUsuarioValidator : AbstractValidator<PutUsuarioCommand>
{
    public PutUsuarioValidator()
    {
        RuleFor(u => u.Nome).NotEmpty()
            .WithMessage("Nome deve ser prenhecido")
            .MaximumLength(100)
            .WithMessage("Deve Conter apenas 100 caracteres")
            .NotNull();
        
        RuleFor(u=>u.Email).NotEmpty().WithMessage("Não deve ser vazio")
            .EmailAddress()
            .WithMessage("E-mail deve ser informado");
        

    }
}