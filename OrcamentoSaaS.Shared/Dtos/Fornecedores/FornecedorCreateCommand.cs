namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorCreateCommand(
    Guid TenantId,
    string Nome,
    string Cidade,
    string Uf,
    string Email,
    string? Telefone,
    string Documento,
    decimal PorcentagemAVista,
    decimal PorcentagemAPrazo);