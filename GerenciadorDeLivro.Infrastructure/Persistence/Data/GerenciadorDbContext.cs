using GerenciadorDeLivro.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeLivro.Infrastructure.Persistence.Data;

public class GerenciadorDbContext :DbContext
{

    public GerenciadorDbContext(DbContextOptions<GerenciadorDbContext> options) : base(options)
    {
        
    }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GerenciadorDbContext).Assembly);
    }

   
        
}