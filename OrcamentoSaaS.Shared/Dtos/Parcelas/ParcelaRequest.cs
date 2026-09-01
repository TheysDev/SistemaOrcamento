using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Parcelas;

public sealed record ParcelaRequest
{
   [Required]
   public Guid ParcelaId { get; init; }
   [Required]
   public DateOnly DataPagamento { get; init; }
}