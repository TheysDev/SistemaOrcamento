using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrcamentoSaaS.Api.Data;

public class ProdutoConfig : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("tb_Produtos");
        
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => new {p.TenantId, p.Codigo}).IsUnique();
        
        builder.Property(p => p.Codigo).IsRequired();
        builder.Property(p => p.Valor).IsRequired().HasPrecision(12, 2);
        builder.Property(p => p.Descricao).IsRequired().HasMaxLength(150);
        builder.Property(o => o.IsActive).IsRequired();

        builder.ComplexProperty(p => p.Detalhes, 
            d => d.ToJson().IsRequired());
    }
}