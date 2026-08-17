using OrcamentoSaaS.Api.Utils;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Queries;

public class GetBuscarOrcamentosHandler(AppDbContext db)
{
    public async Task<Result<PaginacaoResponse<OrcamentoResponse>>> Handle(int pagina, int tamanhoPagina, CancellationToken ct)
    {
        var query = db.Orcamentos
            .AsNoTracking()
            .OrderBy(o => o.Codigo)
            .Select(o => new OrcamentoResponse(
                o.Id,
                new ClienteResumoResponse(
                    o.Cliente.Id,
                    o.Cliente.Nome),
                new FornecedorResumoResponse(
                    o.Fornecedor.Id,
                    o.Fornecedor.Nome),
                o.CodigoFormatado,
                o.Validade,
                o.Status));

        var orcamentosPaginados = await query.PaginarAsync(pagina, tamanhoPagina, ct);

        return Result<PaginacaoResponse<OrcamentoResponse>>.Success(orcamentosPaginados);
    }
}