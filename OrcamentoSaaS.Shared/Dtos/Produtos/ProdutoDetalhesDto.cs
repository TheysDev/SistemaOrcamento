using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public class ProdutoDetalhesDto
{
    [property:NotDecimalMenorZero(ErrorMessage = "O comprimento não pode ser zero ou menor que zero.")]
    public decimal? Comprimento { get; set; }
    
    [property:NotDecimalMenorZero(ErrorMessage = "O peso não pode ser zero ou menor que zero.")]
    public decimal? Peso { get; set; }
    
    [property:MaxLength(length: 20)]
    public string? Diametro { get; set; }
    
    [property:NotDecimalMenorZero(ErrorMessage = "O volume não pode ser zero ou menor que zero.")]
    public decimal? Volume { get; set; }
}
   