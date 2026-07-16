using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Api.Features.Produtos.Shared;

public static class ProdutoResponseMappings
{
    public static ProdutoResponse ToResponse(this Produto produto)
    {
        return new ProdutoResponse(
            produto.Id, 
            produto.Codigo,
            produto.Descricao,
            produto.Valor,
            new ProdutoDetalhesDto(
                produto.Detalhes.Comprimento,
                produto.Detalhes.Peso,
                produto.Detalhes.Diametro,
                produto.Detalhes.Volume));
    }
}