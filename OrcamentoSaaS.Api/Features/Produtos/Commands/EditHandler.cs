using OrcamentoSaaS.Api.Features.Produtos.Shared;
using OrcamentoSaaS.Shared.Dtos.Produtos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Produtos.Commands;

public class EditHandler(AppDbContext db)
{
    public async Task<Result<ProdutoResponse>> Handle(
            ProdutoEditCommand cmd, 
            CancellationToken ct)
    {
        var produto = await db.Produtos.FirstOrDefaultAsync(p => p.Id == cmd.Id, ct);
        
        if(produto is null)
            return Result<ProdutoResponse>.Fail("Produto não encontrado");

        var detalhesResult = ProdutoDetalhes.Criar(
            cmd.Detalhes.Comprimento,
            cmd.Detalhes.Peso,
            cmd.Detalhes.Volume,
            cmd.Detalhes.Diametro);

        if (detalhesResult.IsFailure)
            return Result<ProdutoResponse>.Fail(detalhesResult.Error);
        
        var detalhes = detalhesResult.Value;

        produto.EditarDados(cmd.Descricao, cmd.Valor, detalhes);

        await db.SaveChangesAsync(ct);
        
        var response = produto.ToResponse();
        
        return Result<ProdutoResponse>.Success(response);
    }
}