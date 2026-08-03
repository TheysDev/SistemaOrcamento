namespace OrcamentoSaaS.Shared.Dtos.Parcelas;

public record ParcelaCommand(
    Guid ParcelaId,
    Guid PedidoId,
    DateOnly DataPagamento);