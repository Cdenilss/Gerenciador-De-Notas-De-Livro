using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Application.Models.ViewModel.AvaliacaoViewModels;

public class AvaliacoesViewModel
{
    public AvaliacoesViewModel(decimal nota, string userName, string tituloLivro, string? descricao, DateTime dataInicioLeitura, DateTime dataFimLeitura)
    {
        Nota = nota;
        UserName = userName;
     
        TituloLivro = tituloLivro;
        Descricao = descricao;
        DataInicioLeitura = dataInicioLeitura;
        DataFimLeitura = dataFimLeitura;
    }
    
    public decimal Nota { get; set; }
    public string UserName{ get; set; }
    public string TituloLivro{ get; set; }
    public string? Descricao { get; set; }
    public DateTime DataInicioLeitura { get; set; }
    public DateTime DataFimLeitura { get; set; }
    
    
    public static AvaliacoesViewModel FromEntity(Avaliacao entity)
    => new (entity.Nota,entity.Usuario.NomeCompleto,entity.Livro.Titulo, entity.Descricao, entity.DataInicioLeitura, entity.DataFimLeitura);
}