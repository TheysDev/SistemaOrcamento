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
    
    public string CodigoFormatado => $"PED - {Codigo:D6}";
    
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
        if (numeroParcelas <= 0)
            return Result<Pedido>.Fail("Número de parcelas inválido.");

        if (valor <= 0)
            return Result<Pedido>.Fail("Valor do pedido inválido.");
        
        var pedido = new Pedido(tenantId, orcamentoId, codigo);

        var result = pedido.GerarParcelas(tenantId, numeroParcelas, valor);
        
        return result.IsFailure 
            ? Result<Pedido>.Fail(result.Error) 
            : Result<Pedido>.Success(pedido);
    }

    private Result GerarParcelas(Guid tenantId, int numeroParcelas, decimal valor)
    {
        var valorParcela = Math.Round(valor / numeroParcelas, 2);
        
        var restante = valor;
        
        for (var i = 1; i <= numeroParcelas; i++)
        {
            var valorAtual = i == numeroParcelas 
                ? restante 
                : valorParcela;
            
            var vencimento = DateOnly.FromDateTime(DateTime.Today.AddMonths(i));
            
            _parcelas.Add(new Parcela(tenantId, i, valorAtual,vencimento));
            
            restante -= valorAtual;
        }
        
        ValorTotal = _parcelas.Sum(p => p.Valor);
        
        return Result.Success();
    }

    public Result PagarParcelas(Guid parcelaId, DateOnly dataPagamento)
    {
        var parcela = Parcelas.FirstOrDefault(x => x.Id == parcelaId);

        if (parcela is null)
            return Result.Fail("Parcela não encontrada.");
        
        var result = parcela.Pagar(dataPagamento);

        if (result.IsFailure)
            return result;

        if (Parcelas.All(p => p.Paga))
            Status = StatusPedido.Pago;

        return Result.Success();
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