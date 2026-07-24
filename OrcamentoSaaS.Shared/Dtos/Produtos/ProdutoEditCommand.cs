namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public sealed record ProdutoEditCommand(
    Guid Id,
    Guid TenantId,
    string Descricao,
    decimal Valor,
    ProdutoDetalhesDto Detalhes);