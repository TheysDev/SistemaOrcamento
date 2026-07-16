namespace OrcamentoSaaS.Api.Domain.Entities;

public class Pedido
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public DateOnly Data { get; private set; }
    public StatusPedido Status { get; private set; }
    
    public Guid OrcamentoId { get; private set; }
    public Orcamento Orcamento { get; private set; } = null!;

    private readonly List<Parcela> _parcelas = [];
    public IReadOnlyList<Parcela> Parcelas => _parcelas.AsReadOnly();
    
    public decimal ValorTotal => _parcelas.Sum(p => p.Valor);
    
    public bool IsActive { get; private set; }
    
    protected Pedido()
    {}

    private Pedido(Guid tenantId, Guid orcamentoId)
    {
        TenantId = tenantId;
        OrcamentoId = orcamentoId;
        Data = DateOnly.FromDateTime(DateTime.UtcNow);
        Status = StatusPedido.Aberto;
    }

    public static Result<Pedido> Criar(Guid tenantId, Guid orcamentoId, int numeroParcelas, decimal valor)
    {
        var pedido = new Pedido(tenantId, orcamentoId);
        
        var valorParcela = Math.Round(valor / numeroParcelas, 2);
        
        var restante = valor;
        
        for (var i = 1; i <= numeroParcelas; i++)
        {
            var valorAtual = i == numeroParcelas 
                ? restante 
                : valorParcela;
            
            var vencimento = DateOnly.FromDateTime(DateTime.Today.AddMonths(i));
            
            pedido._parcelas.Add(new Parcela(tenantId, i, valorAtual,vencimento));
            
            restante -= valorAtual;
        }
        
        return Result<Pedido>.Success(pedido);
    }
    
    
    public void Ativar()
    {
        IsActive = true;
    }
    
    public void Inativar()
    {
        IsActive = false;
    }
}