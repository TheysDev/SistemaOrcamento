using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Commands;

public class DeleteHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id,
        CancellationToken ct)
    {
        var fornecedor = await db.Fornecedores.FirstOrDefaultAsync(f=> f.Id == id, ct);

        if (fornecedor is null)
            return Result.Fail("Fornecedor não encontrado");
        
        fornecedor.Inativar();
        
        await db.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}