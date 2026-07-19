using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Domain.Entities;

public class ItemOrcamento
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }

    public Guid OrcamentoId { get; private set; }
    public Orcamento Orcamento { get; private set; } = null!;

    public Guid ProdutoId { get; private set; }
    public Produto Produto { get; private set; } = null!;

    public int Quantidade { get; private set; }
    public decimal Valor { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal Total => Quantidade * Valor - Desconto;
    public bool IsActive { get; private set; }


    protected ItemOrcamento()
    {}

    private ItemOrcamento(Guid tenantId,Guid produtoId, 
        int quantidade, decimal valor, decimal desconto)
    {
        TenantId = tenantId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        Valor = valor;
        Desconto = desconto;
        IsActive = true;
    }
    
    public static Result<ItemOrcamento> Criar(Guid tenantId, Guid produtoId, 
        int quantidade, decimal valor, decimal desconto)
    {
        if (produtoId == Guid.Empty)
            return Result<ItemOrcamento>.Fail("O produto deve ser informado");
        
        if (quantidade <= 0)
            return Result<ItemOrcamento>.Fail("Quantidade deve ser maior que 0.");

        if (valor <= 0)
            return Result<ItemOrcamento>.Fail("Valor deve ser maior que 0.");
        
        var item = new ItemOrcamento(tenantId ,produtoId, quantidade, valor, desconto);
        
        return Result<ItemOrcamento>.Success(item);
    }

    public void Ativar() => IsActive = true;

    public void Inativar() => IsActive = false;
}