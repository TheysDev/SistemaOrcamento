namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public record ProdutoCreateCommand(
    Guid TenantId,
    int Codigo,
    string Descricao,
    decimal Valor,
    ProdutoDetalhesDto Detalhes);