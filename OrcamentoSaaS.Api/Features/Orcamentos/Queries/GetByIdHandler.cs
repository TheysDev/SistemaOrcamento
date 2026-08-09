using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Queries;

public class GetByIdHandler(AppDbContext db)
{
    public async Task<Result<OrcamentoResponse>> Handle(Guid id, CancellationToken ct)
    {
        var response = await db.Orcamentos
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OrcamentoResponse(
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
                    i.Valor,
                    i.Desconto,
                    i.Total))
                    .ToList(),
                o.CodigoFormatado,
                o.Validade,
                o.NumeroParcelas,
                o.Total,
                o.Desconto,
                o.Status))
            .FirstOrDefaultAsync(ct);
        
        if (response is null)
            return Result<OrcamentoResponse>.Fail("Orcamento não encontrado");
        
        return Result<OrcamentoResponse>.Success(response);
    }
}