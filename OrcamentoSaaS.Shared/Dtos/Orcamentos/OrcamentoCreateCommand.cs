namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public record OrcamentoCreateCommand(
    Guid TenantId,
    Guid ClienteId,
    Guid FornecedorId,
    DateOnly Validade,
    int NumeroParcelas,
    ICollection<ItemOrcamentoRequest> Itens,
    string? Observacao
);
