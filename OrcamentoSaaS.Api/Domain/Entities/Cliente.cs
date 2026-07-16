namespace OrcamentoSaaS.Api.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cidade { get; private set; } = string.Empty;
    public string Uf { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? Telefone { get; private set; }
    public Documento Documento { get; private set; } = null!;
    public bool IsActive { get; private set; }
    
    protected Cliente()
    {}
    
    internal Cliente(Guid tenantId,string nome, string cidade, string uf, string email, string? telefone, Documento documento)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        Nome = nome;
        Cidade = cidade;
        Uf = uf;
        Email = email;
        Telefone = telefone;
        Documento = documento;
        IsActive = true;
    }
    
    public void Ativar() => IsActive = true;
    
    public void Inativar() => IsActive = false;
}