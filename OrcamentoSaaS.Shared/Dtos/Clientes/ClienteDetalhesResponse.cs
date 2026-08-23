using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Dtos.Pedidos;

namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public sealed record ClienteDetalhesResponse(
    Guid Id,
    ClienteResponse Clientes,
    ICollection<OrcamentoResponse> Orcamentos,
    ICollection<PedidosResponse> Pedidos, 
    int TotalOrcamentos,
    int TotalOrcamentosAprovados,
    int TotalOrcamentosRejeitados,
    int TotalOrcamentosCancelados,
    int TotalPedidos,
    decimal PedidosValorTotal);