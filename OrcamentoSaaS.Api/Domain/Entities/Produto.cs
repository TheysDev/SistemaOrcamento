using OrcamentoSaaS.Shared.Results;

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

    public Result EditarDados(string descricao, decimal valor, ProdutoDetalhes detalhes)
    {
        if (string.IsNullOrEmpty(descricao))
            return Result<Cliente>.Fail("Descrição é obrigatória.");
       
        if (valor <= 0)
            return Result<Cliente>.Fail("Preço não pode ser zero ou menor que zero.");
        
        Descricao = descricao;
        Valor = valor;
        Detalhes = detalhes;

        return Result.Success();
    }
    
    public void Ativar() => IsActive = true;
    
    public void Inativar() => IsActive = false;
  
}