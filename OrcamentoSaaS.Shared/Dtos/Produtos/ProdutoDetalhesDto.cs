using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public sealed record ProdutoDetalhesDto(
    [property:NotDecimalMenorZero(ErrorMessage = "O comprimento não pode ser zero ou menor que zero.")]
    decimal? Comprimento,
    [property:NotDecimalMenorZero(ErrorMessage = "O peso não pode ser zero ou menor que zero.")]
    decimal? Peso,
    [property:MaxLength(length: 20)]
    string? Diametro,
    [property:NotDecimalMenorZero(ErrorMessage = "O volume não pode ser zero ou menor que zero.")]
    decimal? Volume);