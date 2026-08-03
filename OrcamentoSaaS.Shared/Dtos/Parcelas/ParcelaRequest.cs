using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Parcelas;

public sealed record ParcelaRequest
{
   [Required]
   public Guid parcelaId { get; set; }
   [Required]
   public DateOnly dataPagamento { get; set; }
}