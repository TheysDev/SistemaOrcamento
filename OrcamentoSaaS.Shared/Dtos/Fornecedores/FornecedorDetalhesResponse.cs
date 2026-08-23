using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Dtos.Pedidos;

namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorDetalhesResponse(
    Guid Id,
    FornecedorResponse Fornecedor,
    ICollection<OrcamentoResponse> Orcamentos,
    ICollection<PedidosResponse> Pedidos,
    decimal PorcentagemAVista,
    decimal PorcentagemAPrazo,
    int TotalOrcamentos,
    int TotalOrcamentosAprovados,
    int TotalOrcamentosRejeitados,
    int TotalOrcamentosCancelados,
    int TotalPedidos,
    decimal PedidosValorTotal);