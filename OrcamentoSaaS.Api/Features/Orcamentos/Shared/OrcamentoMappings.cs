using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Shared;

public static class OrcamentoMappings
{
    public static Result<Orcamento> ToEntity(this OrcamentoCreateCommand cmd, ICollection<ItemOrcamento> itens, int codigo)
    {
        return Orcamento.Criar(
            cmd.TenantId,
            cmd.ClienteId,
            cmd.FornecedorId,
            codigo,
            cmd.Validade,
            itens,
            cmd.NumeroParcelas);
    }

    public static Result<ItemOrcamento> ToEntity(this ItemOrcamentoRequest req, Guid tenantId, decimal valor)
    {
        return ItemOrcamento.Criar(
            tenantId,
            req.ProdutoId,
            req.Quantidade,
            valor,
            req.Desconto);
    }
}