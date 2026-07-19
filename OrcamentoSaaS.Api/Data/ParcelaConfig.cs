using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrcamentoSaaS.Api.Data;

public class ParcelaConfig : IEntityTypeConfiguration<Parcela>
{
    public void Configure(EntityTypeBuilder<Parcela> builder)
    {
       builder.ToTable("tb_Parcelas");
       
       builder.HasKey(p => p.Id);
       
       builder.Property(p => p.TenantId).IsRequired();
       builder.Property(p => p.Numero).IsRequired();
       builder.Property(p => p.Valor).HasPrecision(18,2).IsRequired();
       
       builder.Property(p => p.Vencimento)
           .HasColumnType("date")
           .IsRequired();
       
       builder.Property(p => p.DataPagamento)
           .HasColumnType("date")
           .IsRequired(false);

       builder.HasOne(p => p.Pedido)
           .WithMany(p => p.Parcelas)
           .HasForeignKey(p => p.PedidoId)
           .OnDelete(DeleteBehavior.Cascade);
       
       builder.Ignore(p => p.Paga);
    }
}