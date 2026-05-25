using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Enums;

namespace GerenciadorDeLivro.Application.Models.ViewModel;

public class LivrosViewModel
{
    public LivrosViewModel(Guid id, string titulo, string descricao, string isbn, string autor, string editora, GeneroEnum genero, decimal? notaMedia, byte? capaLivro, List<Avaliacao>? avaliacaos)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        ISBN = isbn;
        Autor = autor;
        Editora = editora;
        Genero = genero;
        NotaMedia = notaMedia;
        CapaLivro = capaLivro;
        Avaliacaos = avaliacaos.Count;
    }

    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string ISBN { get; private set; }
        public string Autor { get; private set; }
        public string Editora { get; private set; }
        public GeneroEnum Genero  { get; private set; }
        public decimal? NotaMedia { get; private set; }
        public byte? CapaLivro { get; private set; }
        public int Avaliacaos { get; private set; }


        public static LivrosViewModel FromEntity(Livro entity)
        => new (entity.Id,entity.Titulo, entity.Descricao,entity.ISBN,entity.Autor, 
            entity.Editora, entity.Genero, entity.NotaMedia, 
            entity.CapaLivro, entity.AvaliacoesLivro);
        
}