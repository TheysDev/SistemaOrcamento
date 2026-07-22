using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public sealed record ProdutoCreateRequest(
    [property:Required(ErrorMessage = "O Código do Produto é obrigatorio.")]
    int Codigo,
    [property:Required(ErrorMessage = "O Nome do Produto é obrigatorio.")]
    string Descricao,
    [property:Required(ErrorMessage = "O Preço do Produto é obrigatorio.")]
    [property:NotDecimalMenorZero(ErrorMessage = "O preço não pode ser zero ou menor que zero.")]
    decimal Valor,
    ProdutoDetalhesDto Detalhes);


