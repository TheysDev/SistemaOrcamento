namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public sealed record ClienteEditCommand(
    Guid Id,
    Guid TenantId,
    string Nome,
    string Cidade,
    string Uf,
    string Email,
    string? Telefone);