using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Enums;

namespace GerenciadorDeLivro.Application.Models.InputModel;

public class CreateLivrosInputModel
{
      
   
    public string Titulo { get; set; }
    public string Descricao { get;set; }
    public string ISBN { get;set; }
    public string Autor { get; set;}
    public string Editora { get; set;}
    public GeneroEnum Genero  { get;set; }
    public int AnoDePublicacao { get;set; }
    public int QuantidadeDePaginas { get;set; }
    public decimal? NotaMedia { get; set;}
    public byte? CapaLivro { get; set; }
    
    
    public Livro ToEntity()
    =>new(Titulo, Descricao, ISBN, Autor, Editora, Genero, AnoDePublicacao,QuantidadeDePaginas, NotaMedia, CapaLivro);
}
