using System.Text.Json.Serialization;
using GerenciadorDeLivro.Application.Models.InputModel.Converters;
using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Entities;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.AvaliacaoCommands;

public class InsertAvaliacaoCommand : IRequest<ResultViewModel<Guid>>
{
    public InsertAvaliacaoCommand(decimal nota, string? descricao, Guid idUser, Guid idLivro, DateTime dataInicioLeitura, DateTime dataFimLeitura)
    {
        Nota = nota;
        Descricao = descricao;
        IdUser = idUser;
        IdLivro = idLivro;
        DataInicioLeitura = dataInicioLeitura;
        DataFimLeitura = dataFimLeitura;
    }

    public decimal Nota { get; set; }
    public string? Descricao { get; set; }
    public Guid IdUser { get; set; }
    public Guid IdLivro { get; set; }
    [JsonConverter(typeof(FlexibleDateTimeConverter))]
    public DateTime DataInicioLeitura { get; set; }
    [JsonConverter(typeof(FlexibleDateTimeConverter))]
    public DateTime DataFimLeitura { get; set; }

    public Avaliacao ToEntity()
        => new(Nota,Descricao,IdUser,IdLivro,DataInicioLeitura,DataFimLeitura);
}