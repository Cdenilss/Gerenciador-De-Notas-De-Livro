using System.Text.Json.Serialization;
using GerenciadorDeLivro.Application.Models.InputModel.Converters;
using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Application.Models.InputModel;

public class CreateAvaliacaoInputModel
{
    public decimal Nota { get; set; }
    public string Descricao { get; set; }
    public Guid IdUser { get; set; }
    public Guid IdLivro { get; set; }
    [JsonConverter(typeof(FlexibleDateTimeConverter))]
    public DateTime DataInicioLeitura { get; set; }
    [JsonConverter(typeof(FlexibleDateTimeConverter))]
    public DateTime DataFimLeitura { get; set; }

    public Avaliacao ToEntity()
    => new(Nota,Descricao,IdUser,IdLivro,DataInicioLeitura,DataFimLeitura);
    
    
}
