using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrcamentoSaaS.Api.Data;

public class PedidoConfig : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("tb_Pedidos");
        
        builder.HasKey(p => p.Id);
        builder.HasIndex(o => new {o.TenantId, o.Codigo}).IsUnique();
        
        builder.HasIndex(p => new { p.TenantId, p.OrcamentoId, p.IsActive, p.Codigo })
            .HasDatabaseName("IX_Pedidos_Tenant_Orcamento_Codigo")
            .IsDescending(false, false, false, true)
            .IncludeProperties(p => new { p.Data, p.Status, p.ValorTotal });

        builder.Property(p => p.TenantId).IsRequired();
        builder.Property(p => p.Codigo).IsRequired();
        
        builder.Property(p => p.ValorTotal)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne(p => p.Orcamento)
            .WithOne().HasForeignKey<Pedido>(p => p.OrcamentoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);;

    }
}