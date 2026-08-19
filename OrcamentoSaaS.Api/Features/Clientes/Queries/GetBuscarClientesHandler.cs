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

            var documentoResult = Documento.Criar(busca);
            
            if (documentoResult.IsSuccess)
            {
                var documento = documentoResult.Value;

                consulta = consulta.Where(c =>
                    c.Nome.Contains(busca) ||
                    c.Documento == documento);
            }
            else
            {
                consulta = consulta.Where(c =>
                    c.Nome.Contains(busca));
            }
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