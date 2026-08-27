using OrcamentoSaaS.Shared.Dtos.Produtos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Produtos.Queries;

public class GetProdutoByIdHandler(AppDbContext db)
{
    public async Task<Result<ProdutoResponse>> Handle(Guid id, CancellationToken ct)
    {
        var response = await db.Produtos
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProdutoResponse(
                p.Id,
                p.Codigo,
                p.Descricao,
                p.Valor,
                new ProdutoDetalhesDto
                {
                    Comprimento = p.Detalhes.Comprimento,
                    Diametro = p.Detalhes.Diametro,
                    Peso = p.Detalhes.Peso,
                    Volume =  p.Detalhes.Volume
                }
                ))
            .FirstOrDefaultAsync(ct);
        
        return response is null 
            ? Result<ProdutoResponse>.Fail("Produto não encontrado") 
            : Result<ProdutoResponse>.Success(response);
    }
}