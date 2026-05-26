using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Enums;

namespace GerenciadorDeLivro.Application.Models.ViewModel;

public class LivrosItemViewModel
{
    public LivrosItemViewModel( string titulo, string descricao, string autor, string editora, GeneroEnum genero, decimal? notaMedia, byte[]? capaLivro)
    {
        Titulo = titulo;
        Descricao = descricao;
        Autor = autor;
        Editora = editora;
        Genero = genero;
        NotaMedia = notaMedia;
        CapaLivro = capaLivro;
    }


    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string Autor { get; private set; }
    public string Editora { get; private set; }
    public GeneroEnum Genero  { get; private set; }
    public decimal? NotaMedia { get; private set; }
    public byte[]? CapaLivro { get; private set; }
    
    public static LivrosItemViewModel FromEntity(Livro entity)
    => new (entity.Titulo, entity.Descricao,entity.Autor,entity.Editora
        ,entity.Genero,entity.NotaMedia,entity.CapaLivro);

}