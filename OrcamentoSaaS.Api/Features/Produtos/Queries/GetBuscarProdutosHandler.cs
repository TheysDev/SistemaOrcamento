using OrcamentoSaaS.Api.Utils;
using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Dtos.Produtos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Produtos.Queries;

public class GetBuscarProdutosHandler(AppDbContext db)
{
    public async Task<Result<PaginacaoResponse<ProdutoResponse>>> Handle(ProdutoQuery query, CancellationToken ct)
    {
        var consulta = db.Produtos.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Busca))
        {
            var busca = query.Busca.Trim();
            
            var isNumero = int.TryParse(busca, out var codigo);
            
            consulta = consulta.Where(p =>
                p.Descricao.Contains(busca)
                || (isNumero && p.Codigo == codigo));
        }

        var produtos = await consulta
            .OrderBy(p => p.Codigo)
            .Select(p => new ProdutoResponse(
                p.Id,
                p.Codigo,
                p.Descricao,
                p.Valor,
                new ProdutoDetalhesDto
                {
                    Comprimento = p.Detalhes.Comprimento,
                    Peso = p.Detalhes.Peso,
                    Diametro = p.Detalhes.Diametro,
                    Volume = p.Detalhes.Volume
                }))
            .PaginarAsync(query.Pagina, query.TamanhoPagina, ct);

        return Result<PaginacaoResponse<ProdutoResponse>>
            .Success(produtos);
    }
}