using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeLivro.Infrastructure.Persistence.Repository;

public class LivroRepository : ILivroRepository
{
    private readonly GerenciadorDbContext _context;

    public LivroRepository(GerenciadorDbContext context)
    {
        _context = context;
    }

    public async Task<List<Livro>> GetAll()
    {
        var livros = await _context.Livros.Where(l=>!l.IsDeleted)
            .ToListAsync();
        return livros;
        
    }

    public async Task<Livro?> GetById(Guid id)
    {
     return await _context.Livros.Where(l => !l.IsDeleted).SingleOrDefaultAsync(l=>l.Id == id);
    }

    public async Task<Livro?> GetDetailsById(Guid id)
    {
       var livro=  await _context.Livros.Where(l => !l.IsDeleted).Include(l=>l.AvaliacoesLivro)
            .SingleOrDefaultAsync(l => l.Id == id);
        return livro;
        
    }

    public Task<bool> Exists(Guid id)
    {
        var exist= _context.Livros.AnyAsync(l => l.Id == id);
        return exist;
    }

    public async Task<bool> ExisteIsbnAsync(string isbn, CancellationToken cancellationToken)
    {
     return await _context.Livros.Where(l => !l.IsDeleted).AnyAsync(l => l.ISBN == isbn, cancellationToken);
     
    }

    public async Task<Guid> Add(Livro livro)
    {
        await _context.Livros.AddAsync(livro);
        
        return livro.Id;
    }

    public async Task InsertAvalicao(Avaliacao avaliacao)
    {
        await _context.Avaliacoes.AddAsync(avaliacao);
    }
    public async Task Update(Livro livro)
    {
       _context.Livros.Update(livro);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
       await _context.SaveChangesAsync(cancellationToken);
    }
}