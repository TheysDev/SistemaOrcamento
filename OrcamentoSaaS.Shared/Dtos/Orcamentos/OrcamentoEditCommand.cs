namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public sealed record OrcamentoEditCommand(
    Guid Id,
    Guid TenantId,
    Guid ClienteId,
    Guid FornecedorId,
    DateOnly Validade,
    int NumeroParcelas,
    string? Observacao,
    ICollection<ItemOrcamentoRequest> Itens);