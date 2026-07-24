using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Commands;

public class DeleteHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id,
        CancellationToken ct)
    {
        var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (cliente is null)
            return Result.Fail("Cliente não encontrado");
        
        cliente.Inativar();
        
        await  db.SaveChangesAsync(ct);
        return Result.Success();
    }
}