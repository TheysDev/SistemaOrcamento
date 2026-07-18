namespace OrcamentoSaaS.Api.Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public int Codigo { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public decimal Valor { get; private set; }
    public ProdutoDetalhes Detalhes { get; private set; } = null!;
    public bool IsActive { get; private set; }
    
    public Produto()
    {}

    internal Produto(Guid tenantId, int codigo, string descricao,decimal valor, ProdutoDetalhes detalhes)
    {
        TenantId = tenantId;
        Codigo = codigo;
        Descricao = descricao;
        Valor = valor;
        Detalhes = detalhes;
        IsActive = true;
    }
    
    public void Ativar() => IsActive = true;
    
    public void Inativar() => IsActive = false;
  
}