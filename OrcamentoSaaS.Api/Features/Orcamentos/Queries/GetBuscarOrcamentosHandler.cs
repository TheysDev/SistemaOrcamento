using OrcamentoSaaS.Api.Utils;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Queries;

public class GetBuscarOrcamentosHandler(AppDbContext db)
{
    public async Task<Result<PaginacaoResponse<OrcamentoResponse>>> Handle(OrcamentoQuery query, CancellationToken ct)
    {
        var consulta = db.Orcamentos.AsNoTracking();
            
        if (!string.IsNullOrWhiteSpace(query.Busca))
        {
            var busca = query.Busca.Trim();
            
            var isNumero = int.TryParse(busca, out var codigo);
            
            consulta = consulta.Where(o =>
                o.Fornecedor.Nome.Contains(busca)
                || o.Cliente.Nome.Contains(busca)
                || (isNumero && o.Codigo == codigo));
        }
        
        var orcamentosPaginados = await consulta
            .OrderByDescending(o => o.Codigo)
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
                o.Status)).
            PaginarAsync(query.Pagina, query.TamanhoPagina, ct);

        return Result<PaginacaoResponse<OrcamentoResponse>>.Success(orcamentosPaginados);
    }
}