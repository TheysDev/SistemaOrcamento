using OrcamentoSaaS.Api.Features.Produtos.Shared;
using OrcamentoSaaS.Shared.Dtos.Produtos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Produtos.Commands;

public class ProdutoCreateHandler(AppDbContext db)
{
    public async Task<Result<ProdutoResponse>> Handle(ProdutoCreateCommand cmd, CancellationToken ct)
    {
        var codigoExiste = await db.Produtos.AsNoTracking().AnyAsync(p => 
            p.Codigo == cmd.Codigo, cancellationToken: ct);

        if (codigoExiste)
            return Result<ProdutoResponse>.Fail("Produto já cadastrado com esse código");

        var result = ProdutoDetalhes.Criar(
            cmd.Detalhes.Comprimento,
            cmd.Detalhes.Peso,
            cmd.Detalhes.Volume,
            cmd.Detalhes.Diametro);

        if (result.IsFailure)
            return Result<ProdutoResponse>.Fail(result.Error);
       
        var detalhes = result.Value;

        var produto = cmd.ToEntity(detalhes);
            
        db.Produtos.Add(produto);
        await db.SaveChangesAsync(ct);
            
        return Result<ProdutoResponse>.Success(produto.ToResponse());
    }
}