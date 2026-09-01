namespace OrcamentoSaaS.Shared.Dtos.Parcelas;

public record ParcelaResponse(
    Guid Id,
    int Numero,
    decimal Valor,
    DateOnly? DataPagamento,
    DateOnly Vencimento);