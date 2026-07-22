using OrcamentoSaaS.Api.Features.Clientes.Shared;
using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Edit;

public class ClienteEditHandler(AppDbContext db)
{
    public async Task<Result<ClienteResponse>> Handle(
        ClienteEditCommand cmd,
        CancellationToken ct)
    {
        var cliente = await db.Clientes.FindAsync([cmd.Id], cancellationToken: ct);
        
        if(cliente == null)
            return Result<ClienteResponse>.Fail("Cliente não encontrado!");
        
        var result = cliente.Atualizar(cmd.Nome, cmd.Cidade, cmd.Email, cmd.Telefone);

        if (result.IsFailure)
            return Result<ClienteResponse>.Fail(result.Error);
        
        await db.SaveChangesAsync(ct);

        var response = cliente.ToResponse();
        
        return Result<ClienteResponse>.Success(response);
    }
}