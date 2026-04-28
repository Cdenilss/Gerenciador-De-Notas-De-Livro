using GerenciadorDeLivro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeLivro.Infrastructure.Persistence.Data.Configurations;

public class AvaliacaoConfiguration: IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.ToTable("Avaliacoes");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nota)
            .IsRequired()
            .HasPrecision(2, 1);

        builder.Property(a => a.Descricao)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(a => a.DataInicioLeitura)
            .IsRequired();
        
        builder.Property(a => a.DataFimLeitura)
            .IsRequired();
        

    }
}
