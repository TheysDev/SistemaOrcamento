namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public record ClienteDetalhesResponse(
    Guid Id,
    string Nome,
    string Cidade,
    string Uf,
    string Email,
    string? Telefone,
    string Documento);