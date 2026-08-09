using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Enums;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public record OrcamentoResponse(
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
    
public record ClienteResumoResponse(Guid Id, string Nome);
public record FornecedorResumoResponse(Guid Id, string Nome);
