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
            new ProdutoDetalhesDto
            {
                Comprimento = produto.Detalhes.Comprimento,
                Peso = produto.Detalhes.Peso,
                Diametro = produto.Detalhes.Diametro,
                Volume = produto.Detalhes.Volume
            });
    }
}