using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Queries;

public class GetClienteByIdHandler(AppDbContext db)
{
    public async Task<Result<ClienteResponse>> Handle(Guid id, CancellationToken ct)
    {
        var response = await db.Clientes
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new ClienteResponse(
                c.Id,
                c.Nome,
                c.Cidade,
                c.Uf,
                c.Email,
                c.Telefone,
                c.Documento))
            .FirstOrDefaultAsync(ct);
        
        return response is null 
            ? Result<ClienteResponse>.Fail("Cliente não encontrado") 
            : Result<ClienteResponse>.Success(response);
    }
}