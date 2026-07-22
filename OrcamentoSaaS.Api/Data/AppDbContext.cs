using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrcamentoSaaS.Api.Domain.Entities;
using OrcamentoSaaS.Api.Domain.Interfaces;

namespace OrcamentoSaaS.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenantProvider) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Orcamento> Orcamentos { get; set; }
    public DbSet<ItemOrcamento> ItemOrcamentos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Parcela> Parcelas { get; set; }
    public DbSet<ControleCodigos> ControleDeCodigos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        modelBuilder.Entity<Cliente>().HasQueryFilter("SoftDelete", c => c.IsActive)
            .HasQueryFilter("TenantFilter", c => c.TenantId == tenantProvider.TenantId);
        
        modelBuilder.Entity<Produto>().HasQueryFilter("SoftDelete", c => c.IsActive)
            .HasQueryFilter("TenantFilter", c => c.TenantId == tenantProvider.TenantId);
        
        modelBuilder.Entity<Fornecedor>().HasQueryFilter("SoftDelete", c => c.IsActive)
            .HasQueryFilter("TenantFilter", c => c.TenantId == tenantProvider.TenantId);
        
        modelBuilder.Entity<Orcamento>().HasQueryFilter("SoftDelete", c => c.IsActive)
            .HasQueryFilter("TenantFilter", c => c.TenantId == tenantProvider.TenantId);
        
        modelBuilder.Entity<ItemOrcamento>().HasQueryFilter("SoftDelete", c => c.IsActive)
            .HasQueryFilter("TenantFilter", c => c.TenantId == tenantProvider.TenantId);
        
        modelBuilder.Entity<Pedido>().HasQueryFilter("SoftDelete", c => c.IsActive)
            .HasQueryFilter("TenantFilter", c => c.TenantId == tenantProvider.TenantId);
        
        modelBuilder.Entity<Parcela>().HasQueryFilter("TenantFilter", c => 
            c.TenantId == tenantProvider.TenantId);
    }
}