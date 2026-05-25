using Bogus;
using GerenciadorDeLivro.Application.Commands.UsuarioCommands;
using NSubstitute.ReturnsExtensions;

namespace GerenciadorDeLivro.Test.Faker.CommandsFakes;

public class InsertUsuarioFaker
{
    private static readonly Faker<InsertUsuarioCommand> _insertUsuarioFaker = new Faker<InsertUsuarioCommand>()
        .RuleFor(u => u.Nome, f => f.Name.FullName())
        .RuleFor(u => u.Email, f=>f.Person.FirstName);

    public static InsertUsuarioCommand CreateFakerCommand() => _insertUsuarioFaker.Generate();
}
