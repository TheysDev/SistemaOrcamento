using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrcamentoSaaS.Api.Data;

public class ItemOrcamentoConfig : IEntityTypeConfiguration<ItemOrcamento>
{
    public void Configure(EntityTypeBuilder<ItemOrcamento> builder)
    {
        builder.ToTable("tb_ItemOrcamento");
        
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantidade).IsRequired();
        builder.Property(i => i.Valor).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.Desconto).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.IsActive).IsRequired();
        
        builder.HasOne(i => i.Orcamento)
            .WithMany(o => o.Itens).HasForeignKey(i => i.OrcamentoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(i => i.Produto)
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Ignore(i => i.Total);
    }
}