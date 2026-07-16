namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public sealed record ProdutoDetalhesDto(
    decimal? Comprimento,
    decimal? Peso,
    string? Diametro,
    decimal? Volume);