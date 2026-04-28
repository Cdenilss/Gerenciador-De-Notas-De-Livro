using FluentValidation;
using GerenciadorDeLivro.Application.Commands.LivrosCommands;

namespace GerenciadorDeLivro.Application.Validators.LivroValidators;

public class InsertLivroValidator : AbstractValidator<InsertLivroCommand> 
{
    public InsertLivroValidator()
    {
        RuleFor(livro => livro.Titulo)
            .NotEmpty()
            .WithMessage("o Titulo precisa ser fornecido")
            .MaximumLength(200)
            .WithMessage("o Titulo precisa ter 200 caracteres");
        
        RuleFor(livro => livro.Autor)
            .NotEmpty()
            .WithMessage("autor precisa ser fornecido")
            .MaximumLength(100)
            .WithMessage("autor precisa ter 100 caracteres");

        RuleFor(livro => livro.Genero)
            .IsInEnum()
            .NotEmpty().WithMessage("o Genero precisa ser fornecido");
        RuleFor(livro => livro.QuantidadeDePaginas)
            .GreaterThan(0).WithMessage("a quantidade de pagina deve ser maior que 0");
        RuleFor(livro => livro.AnoDePublicacao).NotEmpty().WithMessage("o Ano De Publicacao precisa ser fornecido")
            .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("o Ano de publicação não pode ser maior que o ano atual");
        RuleFor(livro => livro.Descricao).NotEmpty()
            .WithMessage("Deve ter um breve resumo do livro")
            .MaximumLength(2000).WithMessage("Apenas 2000 caracteres");
    }
    
}