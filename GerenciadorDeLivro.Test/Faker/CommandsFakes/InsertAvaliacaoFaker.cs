using Bogus;
using GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;

namespace GerenciadorDeLivro.Test.Faker.CommandsFakes;

public class InsertAvaliacaoFaker
{
    private static readonly Bogus.Faker _faker = new("pt_BR");

    private static readonly Bogus.Faker<InsertAvaliacaoCommand> _insertAvaliacaoFaker =
        new Bogus.Faker<InsertAvaliacaoCommand>("pt_BR")
            .CustomInstantiator(f =>
            {
                var dataInicioLeitura = f.Date.Past(1, DateTime.Today);
                var dataFimLeitura = f.Date.Between(dataInicioLeitura, DateTime.Today);

                return new InsertAvaliacaoCommand(
                    f.Random.Decimal(1, 5),
                    f.Lorem.Sentence(),
                    f.Random.Guid(),
                    f.Random.Guid(),
                    dataInicioLeitura,
                    dataFimLeitura);
            });

    public static InsertAvaliacaoCommand CreateFakerCommand() => _insertAvaliacaoFaker.Generate();

    public static InsertAvaliacaoCommand CreateFakerCommandNotaMenorQuePermitido()
    {
        var command = CreateFakerCommand();
        command.Nota = _faker.Random.Decimal(-10, 0.99m);

        return command;
    }

    public static InsertAvaliacaoCommand CreateFakerCommandNotaMaiorQuePermitido()
    {
        var command = CreateFakerCommand();
        command.Nota = _faker.Random.Decimal(5.01m, 10);

        return command;
    }

    public static InsertAvaliacaoCommand CreateFakerCommandDataInicioLeituraFutura()
    {
        var command = CreateFakerCommand();
        command.DataInicioLeitura = _faker.Date.Future(1, DateTime.Today);
        command.DataFimLeitura = command.DataInicioLeitura;

        return command;
    }

    public static InsertAvaliacaoCommand CreateFakerCommandDataFimLeituraFutura()
    {
        var command = CreateFakerCommand();
        command.DataFimLeitura = _faker.Date.Future(1, DateTime.Today);

        return command;
    }

    public static InsertAvaliacaoCommand CreateFakerCommandDataFimLeituraMenorQueInicio()
    {
        var command = CreateFakerCommand();
        command.DataInicioLeitura = _faker.Date.Recent(30, DateTime.Today);
        command.DataFimLeitura = command.DataInicioLeitura.AddDays(-1);

        return command;
    }

    public static InsertAvaliacaoCommand CreateFakerCommandDescricaoMaiorQuePermitido()
    {
        var command = CreateFakerCommand();
        command.Descricao = _faker.Random.String2(501);

        return command;
    }

    public static InsertAvaliacaoCommand CreateFakerCommandDescricaoNula()
    {
        var command = CreateFakerCommand();
        command.Descricao = null;

        return command;
    }

    public static InsertAvaliacaoCommand CreateFakerCommandComNotaMinima()
    {
        var command = CreateFakerCommand();
        command.Nota = 1;

        return command;
    }

    public static InsertAvaliacaoCommand CreateFakerCommandComNotaMaxima()
    {
        var command = CreateFakerCommand();
        command.Nota = 5;

        return command;
    }
}
