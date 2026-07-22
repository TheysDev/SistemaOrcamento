using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Commands;

public class DeleteHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id,
        CancellationToken ct)
    {
        var cliente = await db.Clientes.FindAsync([id], ct);

        if (cliente == null)
            return Result.Fail("Cliente não encontrado");
        
        cliente.Inativar();
        
        await  db.SaveChangesAsync(ct);
        return Result.Success();
    }
}