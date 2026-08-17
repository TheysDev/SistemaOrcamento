using OrcamentoSaaS.Api.Utils;
using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Queries;

public class GetBuscarClientesHandler(AppDbContext db)
{
    public async Task<Result<PaginacaoResponse<ClienteResponse>>> Handle(ClienteQuery query, CancellationToken ct)
    {
        var consulta = db.Clientes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Busca))
        {
            var busca = query.Busca.Trim();

            var documento = Documento.Normalizar(busca);
            
            consulta = consulta.Where(c =>
                c.Nome.Contains(busca) ||
                (!string.IsNullOrEmpty(documento) &&
                 c.Documento.Valor == documento));
        }

        var clientesPaginados = await consulta
            .OrderBy(c => c.Nome)
            .Select(c => new ClienteResponse(
                c.Id,
                c.Nome,
                c.Cidade,
                c.Uf,
                c.Email,
                c.Telefone,
                c.Documento))
            .PaginarAsync(query.Pagina, query.TamanhoPagina, ct);

        return Result<PaginacaoResponse<ClienteResponse>>
            .Success(clientesPaginados);
    }
}