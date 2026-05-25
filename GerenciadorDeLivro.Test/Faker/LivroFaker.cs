using GerenciadorDeLivro.Core.Entities;
using Bogus;
using GerenciadorDeLivro.Core.Enums;
using NSubstitute.ReturnsExtensions;

namespace GerenciadorDeLivro.Test.Faker;

public class LivroFaker
{
    private static readonly Bogus.Faker _faker = new();
    public static Livro CreateLivroFaker()
    {
        return new Livro(
            _faker.Commerce.Product()
            ,_faker.Random.String(50),
            _faker.Random.Int(13).ToString(),
            _faker.Person.FullName,
            _faker.Company.CompanyName(),
            _faker.PickRandom<GeneroEnum>(),
            _faker.Random.Int(0,2025),
            _faker.Random.Int(1,1000),
            _faker.Random.Decimal(0,0),
            _faker.Random.Byte()
            );
    }
}