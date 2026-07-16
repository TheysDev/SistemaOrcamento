using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Api.Features.Produtos.Create;

public static class ProdutoMappings
{
    public static Produto ToEntity(this ProdutoCreateCommand cmd)
    {
        return new Produto(
            cmd.TenantId, 
            cmd.Codigo, 
            cmd.Descricao,
            cmd.Valor,
            new ProdutoDetalhes(
                cmd.Detalhes.Comprimento,
                cmd.Detalhes.Peso,
                cmd.Detalhes.Diametro,
                cmd.Detalhes.Volume));
    }
}