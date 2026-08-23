using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Enums;

namespace OrcamentoSaaS.Shared.Dtos.Pedidos;

public sealed record PedidosResponse(
    Guid Id,
    ClienteResumoResponse Cliente,
    FornecedorResumoResponse Fornecedor,
    string Codigo,
    DateOnly DataPedido,
    decimal ValorTotal,
    StatusPedido StatusPedido
    );