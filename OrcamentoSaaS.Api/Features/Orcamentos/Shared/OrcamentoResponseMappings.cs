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
            itensResponse,
            orcamento.CodigoFormatado,
            orcamento.Validade,
            orcamento.NumeroParcelas,
            orcamento.Total,
            orcamento.Desconto,
            orcamento.Status);
    }

    public static ItemOrcamentoResponse ToResponse(this ItemOrcamento itemOrcamento, ProdutoResponse produto)
    {
        return new ItemOrcamentoResponse(
            itemOrcamento.Id,
            itemOrcamento.Produto.Descricao,
            itemOrcamento.Quantidade,
            itemOrcamento.Valor,
            itemOrcamento.Desconto,
            itemOrcamento.Total);
    }
}