using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Domain.Entities;

public class Pedido
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public DateOnly Data { get; private set; }
    public StatusPedido Status { get; private set; }
    public int Codigo { get; private set; }
    public decimal ValorTotal { get; private set; }
    
    public Guid OrcamentoId { get; private set; }
    public Orcamento Orcamento { get; private set; } = null!;

    private readonly List<Parcela> _parcelas = [];
    public IReadOnlyList<Parcela> Parcelas => _parcelas.AsReadOnly();
   
    public bool IsActive { get; private set; }
    
    protected Pedido()
    {}

    private Pedido(Guid tenantId, Guid orcamentoId, int codigo)
    {
        TenantId = tenantId;
        OrcamentoId = orcamentoId;
        Codigo = codigo;
        Data = DateOnly.FromDateTime(DateTime.UtcNow);
        Status = StatusPedido.Aberto;
    }

    public static Result<Pedido> Criar(Guid tenantId, Guid orcamentoId, 
        int numeroParcelas, decimal valor, int codigo)
    {
        var pedido = new Pedido(tenantId, orcamentoId, codigo);
        
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
        
        pedido.ValorTotal = pedido._parcelas.Sum(p => p.Valor);
        
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