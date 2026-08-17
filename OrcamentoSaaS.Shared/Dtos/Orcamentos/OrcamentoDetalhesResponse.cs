using OrcamentoSaaS.Shared.Enums;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public record OrcamentoDetalhesResponse(
    Guid Id,
    ClienteResumoResponse Cliente,
    FornecedorResumoResponse Fornecedor,
    ICollection<ItemOrcamentoResponse> Itens,
    string Codigo,
    DateOnly Validade,
    int NumeroParcelas,
    decimal ValorTotal,
    decimal DescontoTotal,
    StatusOrcamento StatusOrcamento);