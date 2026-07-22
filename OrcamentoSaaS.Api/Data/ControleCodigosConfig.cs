using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrcamentoSaaS.Api.Data;

public class ControleCodigosConfig : IEntityTypeConfiguration<ControleCodigos>
{
    public void Configure(EntityTypeBuilder<ControleCodigos> builder)
    {
        builder.ToTable("tb_ControleCodigos");
        
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.TenanteId).IsUnique();
    }
}