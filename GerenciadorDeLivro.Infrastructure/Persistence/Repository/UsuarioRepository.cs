using GerenciadorDeLivro.Core.Entities;
using GerenciadorDeLivro.Core.Repository;
using GerenciadorDeLivro.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeLivro.Infrastructure.Persistence.Repository;

public class UsuarioRepository :IUsuarioRepository
{
    private readonly GerenciadorDbContext _context;
    public UsuarioRepository(GerenciadorDbContext context)
    {
        _context = context;
    }
    public async Task<List<Usuario>> GetAll()
    {
        var usuarios = await _context.Usuarios.Where(u=>!u.IsDeleted)
            .Include(u=>u.AvaliacoesUserList).ThenInclude(l=>l.Livro)
            .AsNoTracking()
            .ToListAsync();
        return usuarios;
    }
    public async Task<Usuario?> GetDetailsById(Guid id)
    {
        var usuario = await _context.Usuarios
            .Where(u => !u.IsDeleted).Include(u=>u.AvaliacoesUserList).ThenInclude(l=>l.Livro)
            .SingleOrDefaultAsync(u=>u.Id == id);
        return usuario;
    }
    public async Task<Usuario?> GetById(Guid id)
    {
        return await _context.Usuarios.Where(u => !u.IsDeleted).SingleOrDefaultAsync(u=>u.Id == id);
    }
    public async Task<Guid> Add(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return usuario.Id;
        
    }
    public async Task  Update(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        
    }

    public async Task<bool> Exist(Guid id)
    {
       var user= await _context.Usuarios.AnyAsync(u=>u.Id == id);
       return user;
    }

    public async Task<bool> EmailExiste(string email)
    {
        return await  _context.Usuarios.Where(u => !u.IsDeleted).AnyAsync(u => u.Email == email);
        
    }
}