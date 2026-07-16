namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorResponse(
    Guid Id,
    string Nome, 
    string Cidade, 
    string Uf, 
    string Email, 
    string? Telefone, 
    string Documento,
    decimal PorcentagemAVista, 
    decimal PorcentagemAPrazo);