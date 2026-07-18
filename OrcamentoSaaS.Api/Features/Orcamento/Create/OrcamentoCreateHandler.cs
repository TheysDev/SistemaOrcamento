using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamento.Create;

public class OrcamentoCreateHandler(AppDbContext db)
{
    public async Task<Result<OrcamentoResponse>> Handle(
            OrcamentoCommand cmd, 
            CancellationToken ct)
    {
        var clienteExiste = await db.Clientes.AsNoTracking()
            .AnyAsync(c => c.Id == cmd.ClienteId, ct);
        
        if (!clienteExiste)
            return Result<OrcamentoResponse>.Fail("Cliente não encontrado");
        
        var forncedorExiste = await db.Fornecedores.AsNoTracking()
            .AnyAsync(f => f.Id == cmd.ClienteId, ct);
        
        if (!forncedorExiste)
            return Result<OrcamentoResponse>.Fail("Fornecedor não encontrado");

        var produtosId = cmd.Itens.Select(i => i.ProdutoId)
            .Distinct().ToList();
        
        var produtos = db.Produtos.AsNoTracking()
            .Where(p => produtosId.Contains(p.Id)).ToDictionary(k => k.Id, v => v);

        
    }
}