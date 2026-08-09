namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public record ItemOrcamentoResponse(
    Guid Id,
    string ProdutoNome,
    int Quantidade,
    decimal Valor,
    decimal Desconto,
    decimal Total);