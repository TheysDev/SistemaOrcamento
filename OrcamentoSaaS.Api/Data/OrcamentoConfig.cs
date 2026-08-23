using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrcamentoSaaS.Api.Data;

public class OrcamentoConfig : IEntityTypeConfiguration<Orcamento>
{
    public void Configure(EntityTypeBuilder<Orcamento> builder)
    { 
        builder.ToTable("tb_Orcamento", t =>
            {
                t.HasCheckConstraint(
                    "CK_Orcamento_NumeroParcelas",
                    "[NumeroParcelas] BETWEEN 1 AND 36");
            });
        
        builder.HasKey(o => o.Id);
        builder.HasIndex(o => new {o.TenantId, o.Codigo}).IsUnique();
        
        builder.HasIndex(o => new { o.TenantId, o.ClienteId, o.IsActive, o.Codigo })
            .HasDatabaseName("IX_Orcamento_Tenant_Cliente_Codigo")
            .IsDescending(false, false, false, true)
            .IncludeProperties(o => new { o.Status, o.Validade, o.FornecedorId });

        builder.Property(p => p.TenantId).IsRequired();
        builder.Property(o => o.Codigo).IsRequired();
        
        builder.Property(o => o.NumeroParcelas).IsRequired();
        builder.Property(o => o.Validade).HasColumnType("date").IsRequired();
        builder.Property(o => o.Status).HasConversion<string>().IsRequired();
        builder.Property(o => o.IsActive).IsRequired();
        
        
        builder.HasOne(o => o.Cliente)
            .WithMany()
            .HasForeignKey(o => o.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(o => o.Fornecedor)
            .WithMany()
            .HasForeignKey(o => o.FornecedorId)
            .OnDelete(DeleteBehavior.Restrict);;
        
        builder.Ignore(o => o.Valor);
        builder.Ignore(o => o.Desconto);
    }
}