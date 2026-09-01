using OrcamentoSaaS.Api.Features.Clientes.Commands;
using OrcamentoSaaS.Api.Features.Clientes.Queries;
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
            CreateClienteHandler clienteHandler,
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

            var result = await clienteHandler.Handle(command, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.Created($"/{result.Value.Id}", result.Value);
        });

        group.MapPut("/{id:guid}", async (
            Guid id,
            ClienteEditRequest req,
            ITenantProvider tenantProvider, 
            EditClienteHandler clienteHandler,
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

            var result = await clienteHandler.Handle(command, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });
        
        group.MapDelete("/{id:guid}", async (
            Guid id,
            DeleteClienteHandler clienteHandler,
            CancellationToken ct) =>
        {
            var result = await clienteHandler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });
        
        group.MapGet("/", async (
            [AsParameters] ClienteQuery query,
            GetBuscarClientesHandler clientesHandler,
            CancellationToken ct) =>
        {
            var result = await clientesHandler.Handle(query, ct);

            return !result.IsSuccess
                ? Results.NotFound(result.Error)
                : Results.Ok(result.Value);
        });
        
        group.MapGet("/{id:guid}", async (
            Guid id,
            GetClienteByIdHandler clientesHandler,
            CancellationToken ct) =>
        {
            var result = await clientesHandler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.NotFound(result.Error)
                : Results.Ok(result.Value);
        });
        
        group.MapGet("/detalhes/{id:guid}", async (
            Guid id,
            GetBuscarClienteDetalhesHandler buscarClientesHandler,
            CancellationToken ct) =>
        {
            var result = await buscarClientesHandler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.NotFound(result.Error)
                : Results.Ok(result.Value);
        });

        return endpoints;
    }
}