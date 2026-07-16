using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrcamentoSaaS.Api.Data;

public class FornecedorConfig : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.ToTable("tb_Fornecedor");
        
        builder.HasKey(f => f.Id);
        builder.HasIndex(f => new {f.TenantId, f.Documento }).IsUnique();
        
        builder.Property(f => f.Nome).HasMaxLength(150).IsRequired();
        builder.Property(f => f.Cidade).IsRequired().HasMaxLength(50);
        builder.Property(f => f.Uf).IsRequired().HasMaxLength(2);
        builder.Property(f => f.Email).IsRequired().HasMaxLength(50);
        builder.Property(f => f.Telefone).IsRequired().HasMaxLength(12);
        builder.Property(o => o.IsActive).IsRequired();
        
        builder.Property(f => f.PorcentagemAVista).HasColumnType("decimal(10, 2)").IsRequired();
        builder.Property(f => f.PorcentagemAPrazo).HasColumnType("decimal(10, 2)").IsRequired();
        
        builder.Property(f => f.Documento).HasConversion(
                d => d.Valor,
                v => Documento.FromPersistence(v))
            .HasMaxLength(14)
            .IsRequired();
    }
}