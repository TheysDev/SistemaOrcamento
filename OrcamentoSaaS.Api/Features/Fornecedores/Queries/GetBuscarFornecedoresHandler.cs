using OrcamentoSaaS.Api.Utils;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Queries;

public class GetBuscarFornecedoresHandler(AppDbContext db)
{
    public async Task<Result<PaginacaoResponse<FornecedorResponse>>> Handle(FornecedorQuery query, CancellationToken ct)
    {
        var consulta = db.Fornecedores.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Busca))
        {
            var busca = query.Busca.Trim();

            var documento = Documento.Normalizar(busca);
            
            consulta = consulta.Where(f =>
                f.Nome.Contains(busca) ||
                (!string.IsNullOrEmpty(documento) &&
                 f.Documento.Valor == documento));
        }

        var clientesPaginados = await consulta
            .OrderBy(f => f.Nome)
            .Select(f => new FornecedorResponse(
                f.Id,
                f.Nome,
                f.Cidade,
                f.Uf,
                f.Email,
                f.Telefone,
                f.Documento))
            .PaginarAsync(query.Pagina, query.TamanhoPagina, ct);

        return Result<PaginacaoResponse<FornecedorResponse>>
            .Success(clientesPaginados);
    }
}