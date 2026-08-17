namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public record ProdutoQuery(
    string? Busca,
    int Pagina = 1,
    int TamanhoPagina = 10);