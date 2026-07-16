namespace OrcamentoSaaS.Shared.ValueObjects;

public sealed record ProdutoDetalhes
(
    decimal? Comprimento,
    decimal? Peso,
    string? Diametro,
    decimal? Volume
);