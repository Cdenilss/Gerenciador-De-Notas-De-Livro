using GerenciadorDeLivro.Core.Entities;

namespace GerenciadorDeLivro.Application.Models.ViewModel.AvaliacaoViewModels;

public class AvaliacaoItemViewModel
{
    public AvaliacaoItemViewModel( string tituloLivro, decimal nota)
    {
        TituloLivro = tituloLivro;
        Nota = nota;
       
    }
    public string TituloLivro{ get; private set; }
    public decimal Nota { get; private set; }
  

    public static AvaliacaoItemViewModel FromEntity(Avaliacao entity)
        => new ( entity.Livro.Titulo, entity.Nota);
    
}