using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Queries;

public class GetBuscarOrcamentoDetalhesHandler(AppDbContext db)
{
    public async Task<Result<OrcamentoDetalhesResponse>> Handle(Guid id, CancellationToken ct)
    {
        var response = await db.Orcamentos
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OrcamentoDetalhesResponse(
                o.Id,
                new ClienteResumoResponse(
                    o.Cliente.Id,
                    o.Cliente.Nome),
                new FornecedorResumoResponse(
                    o.Fornecedor.Id,
                    o.Fornecedor.Nome),
                o.Itens.Select(i => new ItemOrcamentoResponse(
                    i.Id,
                    i.Produto.Descricao,
                    i.Quantidade,
                    i.ValorItem,
                    i.DescontoItem,
                    i.Total))
                    .ToList(),
                o.CodigoFormatado,
                o.Validade,
                o.NumeroParcelas,
                o.Valor,
                o.Desconto,
                o.Status))
            .FirstOrDefaultAsync(ct);
        
        return response is null 
            ? Result<OrcamentoDetalhesResponse>.Fail("Orcamento não encontrado") 
            : Result<OrcamentoDetalhesResponse>.Success(response);
    }
}