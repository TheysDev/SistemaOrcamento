using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public sealed record OrcamentoCreateRequest
{
    [NotEmptyGuid(ErrorMessage = "Cliente é obrigatório.")]
    public Guid ClienteId { get; init; }

    [NotEmptyGuid(ErrorMessage = "Fornecedor é obrigatório.")]
    public Guid FornecedorId { get; init; }

    [Range(1, 36, ErrorMessage = "Número de parcelas não pode ser 0 ou maior que 36.")]
    public int NumeroParcelas { get; init; }
    
    [Required(ErrorMessage = "Validade é obrigatória.")]
    [ValidadeDate (ErrorMessage = "A data de validade deve ser pelo menos 7 dias no futuro.")]
    public DateOnly Validade { get; init; }

    [MinLength(1, ErrorMessage = "O orçamento deve possuir ao menos um item.")]
    public ICollection<ItemOrcamentoRequest> Itens { get; init; } 

    [MaxLength(1000, ErrorMessage = "Maximo 1.000 caracteres")]
    public string? Observacao { get; init; }
}
    

