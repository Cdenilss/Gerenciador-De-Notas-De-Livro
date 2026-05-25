using GerenciadorDeLivro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeLivro.Infrastructure.Persistence.Data.Configurations;

public class LivroConfiguration : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable("Livros");
        
        builder.HasKey(livro => livro.Id);
        
       builder.Property(l => l.Titulo)
           .IsRequired()
           .HasMaxLength(200);
       builder.Property(l => l.ISBN)
           .IsRequired()
           .HasMaxLength(13);
       builder.Property(l=> l.Autor)
           .IsRequired()
           .HasMaxLength(100);
       builder.Property(l => l.Editora)
           .IsRequired();
       builder.Property(l=>l.Genero)
           .IsRequired();
       builder.Property(l => l.AnoDePublicacao).
           IsRequired();
       builder.Property(l => l.QuatidadeDePaginas)
           .IsRequired();
       builder.Property(l=>l.NotaMedia)
           .IsRequired(false)
           .HasPrecision(2, 1);
       builder.Property(l=>l.CapaLivro)
           .IsRequired(false);
       
       builder.HasIndex(l => l.ISBN).IsUnique();
       builder.HasMany(l=> l.AvaliacoesLivro)
           .WithOne(a => a.Livro)
           .HasForeignKey(a => a.IdLivro)
           .OnDelete(DeleteBehavior.Cascade);





    }
}
