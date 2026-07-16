namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public record ClienteCreateCommand(
    Guid TenantId,
    string Nome,
    string Cidade,
    string Uf,
    string Email,
    string? Telefone,
    string Documento);