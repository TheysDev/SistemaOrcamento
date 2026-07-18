using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public sealed record ItemOrcamentoRequest(
    [NotEmptyGuid(ErrorMessage = "Produto é obrigatório.")]
    Guid ProdutoId,
    
    [Range(1, int.MaxValue,
        ErrorMessage = "A quantidade deve ser maior que 0.")]
    int Quantidade,
    
    [Range(typeof(decimal), "0", "100000",
        ErrorMessage = "O desconto não pode ser negativo.")]
    decimal Desconto);