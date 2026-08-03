using OrcamentoSaaS.Shared.Dtos.Parcelas;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Pedidos.Commands;

public class PagarParcelaHandler(AppDbContext db)
{
    public async Task<Result> Handle(
       ParcelaCommand cmd,
       CancellationToken ct)
    {
        var pedido = await db.Pedidos
            .Include(p => p.Parcelas)
            .FirstOrDefaultAsync(p => p.Id == cmd.PedidoId, ct);

        if (pedido is null)
            return Result.Fail("Peido não encontrato");

        var result = pedido!.PagarParcelas(cmd.ParcelaId, cmd.DataPagamento);

        if (result.IsFailure)
            return Result.Fail(result.Error);

        await db.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}