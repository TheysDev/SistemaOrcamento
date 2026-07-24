using OrcamentoSaaS.Api.Features.Clientes.Commands;
using OrcamentoSaaS.Shared.Dtos.Clientes;

namespace OrcamentoSaaS.Api.Features.Clientes;

public static class ClienteEndPoints
{
    public static IEndpointRouteBuilder MapCliente(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/cliente");

        group.MapPost("/", async (
            ClienteCreateRequest req,
            ITenantProvider tenantProvider,
            CreateHandler handler,
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
                ? Results.BadRequest(new { error = result.Error })
                : Results.Created($"/{result.Value.Id}", result.Value);
        });

        group.MapPut("/{id:guid}", async (
            Guid id,
            ClienteEditRequest req,
            ITenantProvider tenantProvider, 
            EditHandler handler,
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
        
        group.MapDelete("/{id:guid}", async (
            Guid id,
            DeleteHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });

        return endpoints;
    }
}