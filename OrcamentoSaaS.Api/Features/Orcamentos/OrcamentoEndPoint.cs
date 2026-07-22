using Microsoft.AspNetCore.Mvc;
using OrcamentoSaaS.Api.Features.Orcamentos.Commands;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;

namespace OrcamentoSaaS.Api.Features.Orcamentos;

public static class OrcamentoEndPoint
{
    public static IEndpointRouteBuilder MapOrcamento(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/orcamento");
        
        group.MapPost("/", async (
            [FromBody] OrcamentoRequest req, 
            [FromServices] ITenantProvider tenantProvider,
            [FromServices] CreateHandler handler,
            CancellationToken ct) =>
        {
            var command = new OrcamentoCommand(
                tenantProvider.TenantId,
                req.ClienteId,
                req.FornecedorId,
                req.Validade,
                req.NumeroParcelas,
                req.Itens);
            
            var result = await handler.Handle(command, ct);

            return result.IsSuccess
                ? Results.Created($"/{result.Value.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        });

        return endpoints;
    }
}