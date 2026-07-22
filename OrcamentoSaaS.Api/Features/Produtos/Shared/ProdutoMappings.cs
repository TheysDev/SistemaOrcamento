using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Api.Features.Produtos.Shared;

public static class ProdutoMappings
{
    public static Produto ToEntity(this ProdutoCreateCommand cmd, ProdutoDetalhes detalhes)
    {
        return new Produto(
            cmd.TenantId,
            cmd.Codigo,
            cmd.Descricao,
            cmd.Valor,
            detalhes);
    }
}