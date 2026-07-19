using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public record ItemOrcamentoResponse(
    Guid Id,
    ProdutoResponse Produto,
    int Quantidade,
    decimal Valor,
    decimal Desconto,
    decimal Total);