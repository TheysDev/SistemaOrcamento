using OrcamentoSaaS.Api.Features.Clientes.Shared;
using OrcamentoSaaS.Api.Features.Fornecedores.Shared;
using OrcamentoSaaS.Api.Features.Orcamentos.Shared;
using OrcamentoSaaS.Api.Features.Produtos.Shared;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Commands;

public class CreateHandler(AppDbContext db)
{
    public async Task<Result<OrcamentoResponse>> Handle(
            OrcamentoCreateCommand cmd, 
            CancellationToken ct)
    {
        var cliente = await db.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == cmd.ClienteId, ct);
        
        if (cliente is null)
            return Result<OrcamentoResponse>.Fail("Cliente não encontrado");
        
        var fornecedor = await db.Fornecedores.AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == cmd.FornecedorId, ct);
        
        if (fornecedor is null)
            return Result<OrcamentoResponse>.Fail("Fornecedor não encontrado");

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
        
        await using var transaction = await db.Database.BeginTransactionAsync(ct);

        try
        {
            var controle = await db.ControleDeCodigos
                .FromSqlInterpolated($"SELECT * FROM tb_ControleCodigos WITH (UPDLOCK) WHERE TenanteId = {cmd.TenantId}")
                .FirstOrDefaultAsync(ct);
            
            if (controle is null)
            {
                controle = new ControleCodigos(cmd.TenantId);
                db.ControleDeCodigos.Add(controle);
            }
            
            controle.GerarProximoOrcamento();
            var codigo = controle.CodigoOrcamento!.Value;
            
            var result = cmd.ToEntity(itensValidos, codigo);
            
            if (result.IsFailure)
            {
                await transaction.RollbackAsync(ct);
                return Result<OrcamentoResponse>.Fail(result.Error);
            }
           
            var orcamento = result.Value;
            db.Orcamentos.Add(orcamento);
            
            await db.SaveChangesAsync(ct);
            
            await transaction.CommitAsync(ct);
            
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
        catch (Exception)
        {
            Console.WriteLine("Erro ao criar");
            throw;
        }
    }
}