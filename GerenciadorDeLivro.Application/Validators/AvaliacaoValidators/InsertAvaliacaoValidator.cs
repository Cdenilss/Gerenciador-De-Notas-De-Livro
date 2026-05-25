using FluentValidation;
using GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;

namespace GerenciadorDeLivro.Application.Validators.AvaliacaoValidators;

public class InsertAvaliacaoValidator : AbstractValidator<InsertAvaliacaoCommand> 
{
    public InsertAvaliacaoValidator()
    {
        RuleFor(a => a.Nota)
            .InclusiveBetween(1, 5)
            .WithMessage("Nota deve ser definida de 1 ate 5");
        RuleFor(a=>a.DataInicioLeitura)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Data de inicio deve ser menor ou igual que a data de hoje");
        RuleFor(a => a.DataFimLeitura)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("A data de fim da leitura não pode ser maior que a data atual.")
            .GreaterThanOrEqualTo(a => a.DataInicioLeitura)
            .WithMessage("A data de fim da leitura deve ser maior ou igual à data de início da leitura.");
        RuleFor(a=>a.Descricao)
            .MaximumLength(500)
            .WithMessage("descrição deve ter ate 500 caracteres");
        
    }
}