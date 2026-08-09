using OrcamentoSaaS.Api.Utils;
using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Queries;

public class GetAllClientesHandler(AppDbContext db)
{
    public async Task<Result<PaginacaoResponse<ClienteResponse>>> Handle(int pagina, int tamanhoPagina, CancellationToken ct)
    {
        var query = db.Clientes
            .OrderBy(c => c.Nome)
            .Select(c => new ClienteResponse(
                c.Id,
                c.Nome,
                c.Cidade,
                c.Uf,
                c.Email,
                c.Telefone,
                c.Documento));

        var clientesPaginados = await query.PaginarAsync(pagina, tamanhoPagina, ct);

        return Result<PaginacaoResponse<ClienteResponse>>.Success(clientesPaginados);
    }
}