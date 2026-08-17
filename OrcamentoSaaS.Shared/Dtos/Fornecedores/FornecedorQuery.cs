namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorQuery(
    string? Busca,
    int Pagina = 1,
    int TamanhoPagina = 10);