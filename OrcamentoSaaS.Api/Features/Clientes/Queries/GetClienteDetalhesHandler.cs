using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Queries;

public class GetClienteDetalhesHandler(AppDbContext db)
{
    public async Task<Result<ClienteDetalhesResponse>> Handle(Guid id, CancellationToken ct)
    {
        var response = await db.Clientes
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new ClienteDetalhesResponse(
                c.Id,
                c.Nome,
                c.Cidade,
                c.Uf,
                c.Email,
                c.Telefone,
                c.Documento))
            .FirstOrDefaultAsync(ct);
        
        return response is null 
            ? Result<ClienteDetalhesResponse>.Fail("Cliente não encontrado") 
            : Result<ClienteDetalhesResponse>.Success(response);
    }
}