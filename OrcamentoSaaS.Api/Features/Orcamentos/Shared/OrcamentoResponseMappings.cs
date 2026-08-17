using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Shared;

public static class OrcamentoResponseMappings
{
    public static OrcamentoResponse ToResponse(this Orcamento orcamento, 
        ICollection<ItemOrcamentoResponse> itensResponse,
        ClienteResumoResponse clienteResumo,
        FornecedorResumoResponse fornecedorResumo)
    {
        return new OrcamentoResponse(
            orcamento.Id,
            clienteResumo,
            fornecedorResumo,
            orcamento.CodigoFormatado,
            orcamento.Validade,
            orcamento.Status);
    }

    public static ItemOrcamentoResponse ToResponse(this ItemOrcamento itemOrcamento, ProdutoResponse produto)
    {
        return new ItemOrcamentoResponse(
            itemOrcamento.Id,
            itemOrcamento.Produto.Descricao,
            itemOrcamento.Quantidade,
            itemOrcamento.ValorItem,
            itemOrcamento.DescontoItem,
            itemOrcamento.Total);
    }
}