using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Commands;

public class DeleteHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id,
        CancellationToken ct)
    {
        var orcamento = await db.Orcamentos.FirstOrDefaultAsync(o => o.Id == id, ct);
        
        if (orcamento is null)
            return Result.Fail("Orcamento não encontrato");

        if (orcamento.Status != StatusOrcamento.Rascunho)
            return Result.Fail("É possivel deletar orcamento apenas com status: Rascunho");

        orcamento.Inativar();
        
        await  db.SaveChangesAsync(ct);
        return Result.Success();
    }
}