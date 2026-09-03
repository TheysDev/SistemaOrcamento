using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Commands;

public class AprovarHandler(AppDbContext db)
{
    public async Task<Result> Handle(
        Guid id,
        Guid tenantId,
        CancellationToken ct)
    {
        var orcamento = await db.Orcamentos
            .Include(o => o.Itens)
            .FirstOrDefaultAsync(o => o.Id == id, ct);

        if (orcamento is null)
            return Result.Fail("Orcamento não encontrado");
        
        await using var transaction = await db.Database.BeginTransactionAsync(ct);

        try
        {
            var controle = await db.ControleDeCodigos
                .FirstOrDefaultAsync(ct);

            if (controle is null)
            {
                controle = new ControleCodigos(tenantId);
                db.ControleDeCodigos.Add(controle);
            }

            controle.GerarProximoPedido();
            var codigo = controle.CodigoPedido!.Value;
            
            var result = orcamento.Aprovar();

            if (result.IsFailure)
                return Result.Fail(result.Error);

            var resultPedido = Pedido.Criar(tenantId, id, orcamento.NumeroParcelas, orcamento.Valor, codigo);

            if (resultPedido.IsFailure)
                return Result.Fail(resultPedido.Error);
            
            db.Pedidos.Add(resultPedido.Value);

            await db.SaveChangesAsync(ct);

            await transaction.CommitAsync(ct);

        }
        catch (Exception)
        {
            Console.WriteLine("Erro ao aprovar");
            throw;
        }

        return Result.Success();
    }
}