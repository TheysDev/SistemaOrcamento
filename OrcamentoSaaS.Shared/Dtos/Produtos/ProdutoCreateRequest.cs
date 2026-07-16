namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public sealed record ProdutoCreateRequest(
    int Codigo,
    string Descricao,
    decimal Valor,
    ProdutoDetalhesDto Detalhes);


