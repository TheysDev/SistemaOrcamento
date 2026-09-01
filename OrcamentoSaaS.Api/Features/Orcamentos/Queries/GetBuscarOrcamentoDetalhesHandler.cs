using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Dtos.Produtos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Queries;

public class GetBuscarOrcamentoDetalhesHandler(AppDbContext db)
{
    public async Task<Result<OrcamentoDetalhesResponse>> Handle(Guid id, CancellationToken ct)
    {
        var response = await db.Orcamentos
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OrcamentoDetalhesResponse(
                o.Id,
                new ClienteResumoResponse(
                    o.Cliente.Id,
                    o.Cliente.Nome),
                new FornecedorResumoResponse(
                    o.Fornecedor.Id,
                    o.Fornecedor.Nome),
                o.Itens.Select(i => new ItemOrcamentoResponse(
                    i.Id,
                    new ProdutoResponse(
                        i.ProdutoId, 
                        i.Produto.Codigo,
                        i.Produto.Descricao,
                        i.Produto.Valor,
                        new ProdutoDetalhesDto
                        {
                            Comprimento = i.Produto.Detalhes.Comprimento,
                            Peso =  i.Produto.Detalhes.Peso,
                            Diametro =  i.Produto.Detalhes.Diametro,
                            Volume =  i.Produto.Detalhes.Volume
                        }),
                    i.Quantidade,
                    i.ValorItem,
                    i.DescontoItem, 
                    (i.Quantidade * i.ValorItem) - i.DescontoItem))
                    .ToList(),
                o.CodigoFormatado,
                o.Validade,
                o.NumeroParcelas,
                o.Valor > 0 
                ? o.Valor
                : o.Itens.Sum(i => (i.Quantidade * i.ValorItem) - i.DescontoItem),
                o.Desconto > 0
                ? o.Desconto
                : o.Itens.Sum(i => i.DescontoItem),
                o.Observacao,
                o.Status))
            .FirstOrDefaultAsync(ct);
        
        return response is null 
            ? Result<OrcamentoDetalhesResponse>.Fail("Orcamento não encontrado") 
            : Result<OrcamentoDetalhesResponse>.Success(response);
    }
}