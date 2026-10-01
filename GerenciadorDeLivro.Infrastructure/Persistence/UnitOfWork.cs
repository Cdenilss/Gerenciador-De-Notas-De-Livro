using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;

namespace GerenciadorDeLivro.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly GerenciadorDbContext _context;

    public UnitOfWork(GerenciadorDbContext context, ILivroRepository livro, IUsuarioRepository usuario, IAvaliacaoRepository avaliacao)
    {
        _context = context;
        Livro = livro;
        Usuario = usuario;
        Avaliacao = avaliacao;
    }

    public ILivroRepository Livro { get; }
    public IUsuarioRepository Usuario { get; }
    public IAvaliacaoRepository Avaliacao { get; }


    public async Task<int> CompleteAsync( CancellationToken cancellationToken = default)
    {
        return await  _context.SaveChangesAsync(cancellationToken);

    }
}