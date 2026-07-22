using Microsoft.AspNetCore.Mvc;
using OrcamentoSaaS.Shared.Dtos.Clientes;

namespace OrcamentoSaaS.Api.Features.Clientes.Create;

public static class ClienteCreateEndPoint
{
    public static void CreateClienteRoute(this WebApplication app)
    {
        app.MapPost("/cliente/cadastrar", async (
            [FromBody]ClienteCreateRequest req, 
            [FromServices]ITenantProvider tenantProvider,
            [FromServices]ClienteCreateHandler handler,
            CancellationToken ct) =>
        {
            var command = new ClienteCreateCommand(
                tenantProvider.TenantId,
                req.Nome,
                req.Cidade,
                req.Uf,
                req.Email,
                req.Telefone,
                req.Documento);
            
            var result = await handler.Handle(command, ct);

            return !result.IsSuccess 
                ? Results.BadRequest(new {error = result.Error}) 
                : Results.Created($"/cliente/{result.Value.Id}", result.Value);
        });
    }
}