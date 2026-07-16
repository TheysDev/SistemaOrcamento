using Microsoft.EntityFrameworkCore;
using OrcamentoSaaS.Api.Features.Produtos.Shared;
using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Api.Features.Produtos.Create;

public class ProdutoCreateHandler(AppDbContext db)
{
    public async Task<Result<ProdutoResponse>> Handle(ProdutoCreateCommand cmd, CancellationToken ct)
    {
        var codigoExiste = await db.Produtos.AnyAsync(p => 
            p.Codigo == cmd.Codigo, cancellationToken: ct);

        if (codigoExiste)
            return Result<ProdutoResponse>.Fail("Produto já cadastrado com esse código");

        var produto = cmd.ToEntity();
            
        db.Produtos.Add(produto);
        await db.SaveChangesAsync(ct);
            
        return Result<ProdutoResponse>.Success(produto.ToResponse());
    }
}