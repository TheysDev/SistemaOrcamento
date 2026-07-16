namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public record ProdutoResponse(
    Guid Id,
    int Codigo,
    string Descricao,
    decimal Valor,
    ProdutoDetalhesDto Detalhes);