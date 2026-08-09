using Microsoft.AspNetCore.Mvc;
using OrcamentoSaaS.Api.Features.Orcamentos.Commands;
using OrcamentoSaaS.Api.Features.Orcamentos.Queries;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;

namespace OrcamentoSaaS.Api.Features.Orcamentos;

public static class OrcamentoEndPoint
{
    public static IEndpointRouteBuilder MapOrcamento(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/orcamento");
        
        group.MapPost("/", async (
            OrcamentoCreateRequest req, 
            ITenantProvider tenantProvider,
            CreateHandler handler,
            CancellationToken ct) =>
        {
            var command = new OrcamentoCreateCommand(
                tenantProvider.TenantId,
                req.ClienteId,
                req.FornecedorId,
                req.Validade,
                req.NumeroParcelas,
                req.Itens,
                req.Observacao);
            
            var result = await handler.Handle(command, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.Created($"/{result.Value.Id}", result.Value);
        });

        group.MapPut("/{id:guid}", async (
            Guid id,
            OrcamentoEditRequest req,
            ITenantProvider tenantProvider,
            EditHandler handler,
            CancellationToken ct) =>
        {
            var command = new OrcamentoEditCommand(
                id,
                tenantProvider.TenantId,
                req.ClienteId,
                req.FornecedorId,
                req.Validade,
                req.NumeroParcelas,
                req.Itens);

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

        group.MapPost("/enviar/{id:guid}", async (
            Guid id,
            EnviarHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });
        
        group.MapPost("/aprovar/{id:guid}", async (
            Guid id,
            ITenantProvider tenantProvider,
            AprovarHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(id,tenantProvider.TenantId, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });
        
        group.MapPost("/rejeitar/{id:guid}", async (
            Guid id,
            RejeitarHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });
        
        group.MapPost("/cancelar/{id:guid}", async (
            Guid id,
            CancelarHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });

        group.MapGet("/{id:guid}", async (
            Guid id,
            GetByIdHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.Ok(result.Value);
        });

        return endpoints;
    }
}