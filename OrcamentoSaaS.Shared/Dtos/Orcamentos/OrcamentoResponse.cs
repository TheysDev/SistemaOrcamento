using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Enums;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public record OrcamentoResponse(
    Guid Id,
    ClienteResponse Cliente,
    FornecedorResponse Fornecedor,
    ICollection<ItemOrcamentoResponse> Itens,
    int Codigo,
    DateOnly Validade,
    int NumeroParcelas,
    decimal ValorTotal,
    decimal DescontoTotal,
    StatusOrcamento StatusOrcamento);