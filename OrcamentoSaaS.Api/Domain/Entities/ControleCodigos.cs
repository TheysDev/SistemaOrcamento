namespace OrcamentoSaaS.Api.Domain.Entities;

public class ControleCodigos
{
    public Guid Id { get; private set; }
    public Guid TenanteId { get; private set; }
    public int? CodigoOrcamento { get; private set; }
    public int? CodigoPedido { get; private set; }
    
    protected ControleCodigos() { }

    public ControleCodigos(int? codigoOrcamento, int? codigoPedido)
    {
        CodigoOrcamento = codigoOrcamento;
        CodigoPedido = codigoPedido;
    }

    public ControleCodigos(Guid tenantId)
    {
        TenanteId = tenantId;
    }
    
    public void GerarProximoOrcamento() => CodigoOrcamento = (CodigoOrcamento ?? 0) + 1;
    
    public void GerarProximoPedido() => CodigoPedido = (CodigoPedido ?? 0) + 1;
    
}