using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Dtos.Produtos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Shared;

public static class OrcamentoResponseMappings
{
    public static OrcamentoResponse ToResponse(this Orcamento orcamento, 
        ICollection<ItemOrcamentoResponse> itensResponse,
        ClienteResponse cliente,
        FornecedorResponse fornecedor)
    {
        return new OrcamentoResponse(
            orcamento.Id,
            cliente,
            fornecedor,
            itensResponse,
            orcamento.Codigo,
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
            produto,
            itemOrcamento.Quantidade,
            itemOrcamento.Valor,
            itemOrcamento.Desconto,
            itemOrcamento.Total);
    }
}