namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public record OrcamentoCommand(
    Guid TenantId,
    Guid ClienteId,
    Guid FornecedorId,
    int NumeroParcelas,
    ICollection<ItemOrcamentoRequest> Itens
);
