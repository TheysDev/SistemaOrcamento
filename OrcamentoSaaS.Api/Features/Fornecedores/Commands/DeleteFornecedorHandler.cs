using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Commands;

public class DeleteFornecedorHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id,
        CancellationToken ct)
    {
        var fornecedor = await db.Fornecedores.FirstOrDefaultAsync(f=> f.Id == id, ct);

        if (fornecedor is null)
            return Result.Fail("Fornecedor não encontrado");
        
        var possuiOrcamentos = await db.Orcamentos
            .AnyAsync(o => o.FornecedorId == id, ct);

        if (possuiOrcamentos)
            return Result.Fail("Não é possível excluir um fornecedor que possui orçamentos cadastrados.");
        
        fornecedor.Inativar();
        
        await db.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}