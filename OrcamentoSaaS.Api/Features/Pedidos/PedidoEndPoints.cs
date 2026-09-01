using OrcamentoSaaS.Api.Features.Pedidos.Commands;
using OrcamentoSaaS.Shared.Dtos.Parcelas;

namespace OrcamentoSaaS.Api.Features.Pedidos;

public static class PedidoEndPoints
{
    public static IEndpointRouteBuilder MapPedido(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/pedido");

        group.MapPost("/{pedidoId:guid}/pagar-parcela", async (
            Guid pedidoId,
            ParcelaRequest req,
            PagarParcelaHandler handler,
            CancellationToken ct) =>
        {
            var command = new ParcelaCommand(
                req.ParcelaId,
                pedidoId,
                req.DataPagamento);
            
            var result = await handler.Handle(command, ct);
            
            return !result.IsSuccess
                ? Results.BadRequest(new { result.Error })
                : Results.NoContent();
        });
        
        return endpoints;
    }
}