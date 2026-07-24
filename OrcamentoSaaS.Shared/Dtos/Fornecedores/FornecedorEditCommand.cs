namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorEditCommand(
    Guid Id,
    Guid TenantId,
    string Nome,
    string Cidade,
    string Uf,
    string Email,
    string? Telefone);