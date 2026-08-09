using OrcamentoSaaS.Api.Utils;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Queries;

public class GetAllFornecedoresHandler(AppDbContext db)
{
    public async Task<Result<PaginacaoResponse<FornecedorResponse>>> Handle(int pagina, int tamanhoPagina, CancellationToken ct)
    {
        var query = db.Fornecedores
            .OrderBy(c => c.Nome)
            .Select(c => new FornecedorResponse(
                c.Id,
                c.Nome,
                c.Cidade,
                c.Uf,
                c.Email,
                c.Telefone,
                c.Documento,
                c.PorcentagemAVista,
                c.PorcentagemAPrazo));

        var fornecedoresPaginados = await query.PaginarAsync(pagina, tamanhoPagina, ct);

        return Result<PaginacaoResponse<FornecedorResponse>>.Success(fornecedoresPaginados);
    }
}