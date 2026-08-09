using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Produtos.Commands;

public class DeleteProdutoHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id, 
        CancellationToken ct)
    {
        var produto = await db.Produtos.FirstOrDefaultAsync(p => p.Id == id, ct);
        
        if(produto is null)
            return Result.Fail("Produto não encontrado!");
        
        produto.Inativar();
        await db.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}