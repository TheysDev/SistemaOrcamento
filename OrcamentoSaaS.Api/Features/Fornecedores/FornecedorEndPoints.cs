using Microsoft.AspNetCore.Mvc;
using OrcamentoSaaS.Api.Features.Fornecedores.Commands;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;

namespace OrcamentoSaaS.Api.Features.Fornecedores;

public static class FornecedorEndPoints
{
    public static IEndpointRouteBuilder MapFornecedor(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/fornecedor");
        
        group.MapPost("/", async (
            [FromBody] FornecedorCreateRequest req,
            [FromServices] ITenantProvider tenantProvider,
            [FromServices] CreateHandler handler,
            CancellationToken ct) =>
        {
            var command = new FornecedorCreateCommand(
                tenantProvider.TenantId,
                req.Nome,
                req.Cidade,
                req.Uf,
                req.Email,
                req.Telefone,
                req.Documento,
                req.PorcentagemAVista,
                req.PorcentagemAPrazo);

            var result = await handler.Handle(command, ct);

            return result.IsSuccess
                ? Results.Created($"/{result.Value.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        });

        return endpoints;
    }
}