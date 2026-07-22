using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public record OrcamentoRequest(
    [NotEmptyGuid(ErrorMessage = "Cliente é obrigatório.")]
    Guid ClienteId,

    [NotEmptyGuid(ErrorMessage = "Fornecedor é obrigatório.")]
    Guid FornecedorId,

    [Range(1, 36, ErrorMessage = "Número de parcelas não pode ser 0 ou maior que 36.")]
    int NumeroParcelas,
    
    [Required(ErrorMessage = "Validade é obrigatória.")]
    [ValidadeDate (ErrorMessage = "A data de validade deve ser pelo menos 7 dias no futuro.")]
    DateOnly Validade,

    [MinLength(1, ErrorMessage = "O orçamento deve possuir ao menos um item.")]
    ICollection<ItemOrcamentoRequest> Itens
);
