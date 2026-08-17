using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public sealed record OrcamentoEditRequest
{
    [NotEmptyGuid(ErrorMessage = "Cliente é obrigatório.")]
    public Guid ClienteId { get; set; }

    [NotEmptyGuid(ErrorMessage = "Fornecedor é obrigatório.")]
    public Guid FornecedorId { get; set; }

    [Range(1, 36, ErrorMessage = "Número de parcelas não pode ser 0 ou maior que 36.")]
    public int NumeroParcelas { get; set; }
    
    [Required(ErrorMessage = "Validade é obrigatória.")]
    [ValidadeDate (ErrorMessage = "A data de validade deve ser pelo menos 7 dias no futuro.")]
    public DateOnly Validade { get; set; }
    
    [MaxLength(1000, ErrorMessage = "Maximo de 1000 caracteres.")]
    public string? Observacao { get; set; }

    [MinLength(1, ErrorMessage = "O orçamento deve possuir ao menos um item.")]
    public ICollection<ItemOrcamentoRequest> Itens { get; set; }
}