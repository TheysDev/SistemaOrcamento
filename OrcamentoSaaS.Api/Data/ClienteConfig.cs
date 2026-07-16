using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrcamentoSaaS.Api.Data;

public class ClienteConfig : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("tb_Cliente");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => new {c.TenantId, c.Documento}).IsUnique();
        
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Telefone).HasMaxLength(12);
        builder.Property(c => c.Cidade).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Uf).IsRequired().HasMaxLength(2);
        builder.Property(o => o.IsActive).IsRequired();
        
        builder.Property(c => c.Documento).HasConversion(
                d => d.Valor,
                v => Documento.FromPersistence(v))
            .HasMaxLength(14)
            .IsRequired();
    }
    
}