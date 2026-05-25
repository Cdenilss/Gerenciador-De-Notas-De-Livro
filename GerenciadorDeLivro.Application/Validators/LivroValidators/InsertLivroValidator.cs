using FluentValidation;
using GerenciadorDeLivro.Application.Commands.LivrosCommands;

namespace GerenciadorDeLivro.Application.Validators.LivroValidators;

public class InsertLivroValidator : AbstractValidator<InsertLivroCommand> 
{
    public InsertLivroValidator()
    {
        RuleFor(livro => livro.Titulo)
            .NotEmpty()
            .WithMessage("O título precisa ser fornecido")
            .MaximumLength(200)
            .WithMessage("O título precisa ter no máximo 200 caracteres");
        
        RuleFor(livro => livro.Autor)
            .NotEmpty()
            .WithMessage("O autor precisa ser fornecido")
            .MaximumLength(100)
            .WithMessage("O autor precisa ter no máximo 100 caracteres");
        
        RuleFor(livro => livro.ISBN)
            .NotEmpty()
            .WithMessage("O ISBN precisa ser fornecido")
            .MaximumLength(13)
            .WithMessage("O ISBN deve ter no máximo 13 caracteres");
        
        RuleFor(livro => livro.Genero)
            .IsInEnum()
            .WithMessage("O gênero informado é invalido");
        RuleFor(livro => livro.QuantidadeDePaginas)
            .GreaterThan(0).WithMessage("A quantidade de páginas deve ser maior que 0");
        RuleFor(livro => livro.AnoDePublicacao).GreaterThanOrEqualTo(0).WithMessage("O ano precisa ser Fornecido ")
            .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("O ano de publicação não pode ser maior que o ano atual");
        RuleFor(livro => livro.Descricao).NotEmpty()
            .WithMessage("Deve ter um breve resumo do livro")
            .MaximumLength(2000).WithMessage("A descrição deve ter no máximo 2000 caracteres");
        
    }
    
    
}
