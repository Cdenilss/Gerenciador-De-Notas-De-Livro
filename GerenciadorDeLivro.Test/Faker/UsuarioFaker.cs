using Bogus;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;

namespace GerenciadorDeLivro.Test.Faker;

public class UsuarioFaker
{
    private static readonly Bogus.Faker _faker = new();

    public static Usuario CreateFakeUser()
    {
        return new Usuario(
            _faker.Person.FirstName,
            _faker.Person.Email
        );
    }
}