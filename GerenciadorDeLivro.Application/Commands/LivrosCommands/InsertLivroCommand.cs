using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Enums;
using MediatR;

namespace GerenciadorDeLivro.Application.Commands.LivrosCommands;

public class InsertLivroCommand : IRequest<ResultViewModel<Guid>>
{
    public string Titulo { get; set; }
    public string Descricao { get;set; }
    public string ISBN { get;set; }
    public string Autor { get; set;}
    public string Editora { get; set;}
    public GeneroEnum Genero  { get;set; }
    public int AnoDePublicacao { get;set; }
    public int QuantidadeDePaginas { get;set; }
    public byte[]? CapaLivro { get; set; }
    
    
    public Livro ToEntity()
        =>new(Titulo, Descricao, ISBN, Autor, Editora, Genero, AnoDePublicacao,QuantidadeDePaginas, CapaLivro);
}