namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorDetalhesResponse(
    Guid Id,
    string Nome, 
    string Cidade, 
    string Uf, 
    string Email, 
    string? Telefone, 
    string Documento,
    decimal PorcentagemAVista,
    decimal PorcentagemAPrazo);