using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public record ProdutoBaseRequest
{
    [property:Required(ErrorMessage = "O Código do Produto é obrigatorio.")]
    public int Codigo { get; set; }
    
    [property:Required(ErrorMessage = "O Nome do Produto é obrigatorio.")]
    public string Descricao  { get; set; } = null!;

    [property:Required(ErrorMessage = "O Preço do Produto é obrigatorio.")]
    [property:NotDecimalMenorZero(ErrorMessage = "O preço não pode ser zero ou menor que zero.")]
    public decimal Valor { get; set; }
    
    public ProdutoDetalhesDto Detalhes { get; set; } = null!;
}