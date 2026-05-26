using System.Data;
using GerenciadorDeLivro.Application.Commands.LivrosCommands;
using Bogus;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Enums;

namespace GerenciadorDeLivro.Test.Faker.CommandsFakes;

public class InsertLivroFaker
{
    private static readonly Bogus.Faker _faker = new("pt_BR");
    private static readonly Faker<InsertLivroCommand> _insertLivroFaker = new Faker<InsertLivroCommand>()
        .RuleFor(l => l.Titulo, f => f.Commerce.Product())
        .RuleFor(l => l.Descricao, f => f.Lorem.Sentence())
        .RuleFor(l => l.ISBN, f => f.Random.Int(13).ToString())
        .RuleFor(l => l.Autor, f => f.Person.FullName)
        .RuleFor(l => l.Editora, f => f.Company.CompanyName())
        .RuleFor(l => l.Genero, f => f.PickRandom<GeneroEnum>())
        .RuleFor(l => l.AnoDePublicacao, f => f.Random.Int(0, 2026))
        .RuleFor(l => l.QuantidadeDePaginas, f => f.Random.Int(1, 1000))
        .RuleFor(l => l.CapaLivro, f => f.Random.Bytes(128));
    
    public static InsertLivroCommand CreateFakerCommand() => _insertLivroFaker.Generate();
    
    
    public static InsertLivroCommand CreateFakerCommandComTituloVazio()
    {
        var command = CreateFakerCommand();
        command.Titulo = " ";
        return command;
    }
    
    public static InsertLivroCommand CreateFakerCommandComTituloMaiorQuePermitido()
    {
        var command = CreateFakerCommand();
        command.Titulo = _faker.Random.String2(201);
        return command;
    }

}