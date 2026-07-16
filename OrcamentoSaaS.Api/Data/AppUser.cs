using Microsoft.AspNetCore.Identity;

namespace OrcamentoSaaS.Api.Data;

public class AppUser : IdentityUser
{
    public Guid TenantId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    
    public AppUser(){}

    public AppUser(Guid tenantId, string nome)
    {
        TenantId = tenantId;
        Nome = nome;
        IsActive = true;
    }
    
    public void Ativar() => IsActive = true;
    
    public void Inativar() => IsActive = false;
    
}