using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Commands;

public class DeleteClienteHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id,
        CancellationToken ct)
    {
        var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (cliente is null)
            return Result.Fail("Cliente não encontrado");
        
        var possuiOrcamentos = await db.Orcamentos
            .AnyAsync(o => o.ClienteId == id, ct);

        if (possuiOrcamentos)
            return Result.Fail("Não é possível excluir um cliente que possui orçamentos cadastrados.");
        
        cliente.Inativar();
        
        await  db.SaveChangesAsync(ct);
        return Result.Success();
    }
}