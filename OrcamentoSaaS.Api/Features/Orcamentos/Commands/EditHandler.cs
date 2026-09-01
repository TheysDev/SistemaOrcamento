using OrcamentoSaaS.Api.Features.Clientes.Shared;
using OrcamentoSaaS.Api.Features.Fornecedores.Shared;
using OrcamentoSaaS.Api.Features.Orcamentos.Shared;
using OrcamentoSaaS.Api.Features.Produtos.Shared;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Commands;

public class EditHandler(AppDbContext db)
{
    public async Task<Result<OrcamentoResponse>> Handle(
        OrcamentoEditCommand cmd,
        CancellationToken ct
    )
    {
        var orcamento = await db.Orcamentos.Include(o => o.Itens)
            .FirstOrDefaultAsync(o => o.Id == cmd.Id, ct);

        if (orcamento is null)
            return Result<OrcamentoResponse>.Fail("Orcamento não encontrado");

        if(orcamento.Status is StatusOrcamento.Aprovado or StatusOrcamento.Cancelado)
            return Result<OrcamentoResponse>.Fail("Não é possivel editar um orcamento aprovado ou cancelado.");
        
        var cliente = await db.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == cmd.ClienteId, ct);
        
        if (cliente is null)
            return Result<OrcamentoResponse>.Fail("Cliente não encontrado");
        
        var fornecedor = await db.Fornecedores.AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == cmd.FornecedorId, ct);
        
        if (fornecedor is null)
            return Result<OrcamentoResponse>.Fail("Fornecedor não encontrado");
        
        var result = orcamento.EditarDados(cmd.ClienteId, cmd.FornecedorId, cmd.Validade, cmd.NumeroParcelas, cmd.Observacao);
        
        if (result.IsFailure)
            return Result<OrcamentoResponse>.Fail(result.Error);
        
        var produtosId = cmd.Itens.Select(i => i.ProdutoId).Distinct().ToList();
        
        var produtosDic = await db.Produtos.AsNoTracking()
            .Where(p => produtosId.Contains(p.Id))
            .ToDictionaryAsync(k => k.Id, v => v, ct);

        if (produtosDic.Count != produtosId.Count)
            return Result<OrcamentoResponse>.Fail("Produto não encontrado");

        var itensResults = cmd.Itens.Select(i => 
        {
            var produto = produtosDic[i.ProdutoId]; 
            return i.ToEntity(cmd.TenantId, produto.Valor); 
        }).ToList();
        
        if (itensResults.Any(r => !r.IsSuccess))
        {
            var erro = itensResults.First(r => !r.IsSuccess).Error;
            return Result<OrcamentoResponse>.Fail(erro);
        }
        
        var itensValidos = itensResults.Select(r => r.Value).ToList();
        
        result = orcamento.SubstituirItens(itensValidos);
        
        if (result.IsFailure)
            return Result<OrcamentoResponse>.Fail(result.Error);
        
        await db.SaveChangesAsync(ct);
        
        var clienteResponse = cliente.ToResumoResponse();
        var fornecedorResponse = fornecedor.ToResumoResponse();
            
        var itensResponse = itensValidos.Select(item => 
        {
            var produto = produtosDic[item.ProdutoId];
            return item.ToResponse(produto.ToResponse());
        }).ToList();

        var orcamentoResponse = orcamento.ToResponse(itensResponse, clienteResponse, fornecedorResponse);
            
        return Result<OrcamentoResponse>.Success(orcamentoResponse);
        
    }
}