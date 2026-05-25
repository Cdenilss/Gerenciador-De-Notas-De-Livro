using System.Runtime.InteropServices.JavaScript;
using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeLivro.Infrastructure.Persistence.Repository;

public class AvaliacaoRepository:IAvaliacaoRepository
{
    private readonly GerenciadorDbContext _context;

    public AvaliacaoRepository(GerenciadorDbContext context)
    {
        _context = context;
    }

    public async Task<List<Avaliacao>> GetAll()
    {
        var avaliacao= await _context.Avaliacoes
            .Where(u=>!u.IsDeleted)
            .Include(a=>a.Livro).AsNoTracking()
            .ToListAsync();
        return avaliacao;
        
    }
    
    public async Task<Avaliacao?> GetById(Guid id)
    {
        return await _context.Avaliacoes.SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Avaliacao?> GetDetailsById(Guid id)
    {
        var avaliacao = await _context.Avaliacoes
            .Include(a => a.Livro)
            .Include(a => a.Usuario)
            .SingleOrDefaultAsync(a => a.Id == id);
        return avaliacao;
        
    }
    
    public async Task<bool> ExistsAvaliacaoByUserId(Guid userId, Guid livroId)
    {
        return await _context.Avaliacoes
            .AnyAsync(a => a.IdUser == userId && a.IdLivro == livroId);
    }

}