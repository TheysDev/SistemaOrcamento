namespace OrcamentoSaaS.Api.Domain.Entities;

public class Parcela
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }

    public int Numero { get; private set; }
    public decimal Valor { get; private set; }
    
    public Guid PedidoId { get; private set; }
    public Pedido Pedido { get; private set; } = null!;

    public DateOnly? DataPagamento { get; private set; }
    public DateOnly Vencimento { get; private set; }
    public bool Paga => DataPagamento.HasValue;

    protected Parcela()
    {}

    public Parcela(Guid tenantId, int numeroParcelas, decimal valor, DateOnly vencimento)
    {
        TenantId = tenantId;
        Numero = numeroParcelas;
        Valor = valor;
        Vencimento = vencimento;
    }
    
    public void PagarParcela()
    {
        DataPagamento = DateOnly.FromDateTime(DateTime.Now);;
    }
}