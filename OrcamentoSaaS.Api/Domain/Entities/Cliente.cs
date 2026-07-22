using OrcamentoSaaS.Shared.Results;

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
        TenantId = tenantId;
        Nome = nome;
        Cidade = cidade;
        Uf = uf;
        Email = email;
        Telefone = telefone;
        Documento = documento;
        IsActive = true;
    }

    public Result Atualizar(string nome, string cidade, string email, string? telefone)
    {
        if (string.IsNullOrEmpty(nome))
            return Result<Cliente>.Fail("Nome é obrigatório.");
       
        if (string.IsNullOrEmpty(cidade))
            return Result<Cliente>.Fail("Cidade é obrigatório.");
       
        if (string.IsNullOrEmpty(email))
            return Result<Cliente>.Fail("E-mail é obrigatório.");
        
        Nome = nome.Trim();
        Cidade = cidade.Trim();
        Email = email.Trim();
        if (telefone != null) Telefone = telefone.Trim();

        return Result.Success();
    }
    
    public void Ativar() => IsActive = true;
    
    public void Inativar() => IsActive = false;
}