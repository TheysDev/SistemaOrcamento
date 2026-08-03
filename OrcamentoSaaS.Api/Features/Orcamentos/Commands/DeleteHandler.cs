using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Commands;

public class DeleteHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id,
        CancellationToken ct)
    {
        var orcamento = await db.Orcamentos
            .Include(o => o.Itens)
            .FirstOrDefaultAsync(o => o.Id == id, ct);
        
        if (orcamento is null)
            return Result.Fail("Orcamento não encontrato");
        
        var result = orcamento.Inativar();
        
        if (result.IsFailure)
            return Result.Fail(result.Error);
        
        await  db.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}