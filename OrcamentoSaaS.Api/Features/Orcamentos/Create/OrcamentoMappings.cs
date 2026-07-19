using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Create;

public static class OrcamentoMappings
{
    public static Result<Orcamento> ToEntity(this OrcamentoCommand cmd, ICollection<ItemOrcamento> itens)
    {
        return Orcamento.Criar(
            cmd.TenantId,
            cmd.ClienteId,
            cmd.FornecedorId,
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