using Microsoft.AspNetCore.Mvc;
using OrcamentoSaaS.Shared.Dtos.Clientes;

namespace OrcamentoSaaS.Api.Features.Clientes.Edit;

public static class ClienteEditEndPoint
{
    public static void EditClienteRoute(this WebApplication app)
    {
        app.MapPut("cliente/editar", async (
            [FromRoute] Guid id,
            [FromBody] ClienteEditRequest req,
            [FromServices] ITenantProvider tenantProvider,
            [FromServices] ClienteEditHandler handler,
            CancellationToken ct
        ) =>
        {
            var command = new ClienteEditCommand(
                id,
                tenantProvider.TenantId,
                req.Nome,
                req.Cidade,
                req.Uf,
                req.Email,
                req.Telefone);
            
            var result = await handler.Handle(command, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });
    }
}