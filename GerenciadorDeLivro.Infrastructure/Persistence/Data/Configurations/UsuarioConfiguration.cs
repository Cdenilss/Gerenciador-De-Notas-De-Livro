using GerenciadorDeLivro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeLivro.Infrastructure.Persistence.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.NomeCompleto)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasMany(u => u.AvaliacoesUserList)
            .WithOne(a => a.Usuario)
            .HasForeignKey(a => a.IdUser)
            .OnDelete(DeleteBehavior.Cascade);;

    }
}