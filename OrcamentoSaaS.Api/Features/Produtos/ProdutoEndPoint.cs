using Microsoft.AspNetCore.Mvc;
using OrcamentoSaaS.Api.Features.Produtos.Commands;
using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Api.Features.Produtos;

public static class ProdutoEndPoint
{
    public static IEndpointRouteBuilder MapProduto(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/produto");
        
        group.MapPost("/", async (
            [FromBody] ProdutoCreateRequest req,
            [FromServices] ITenantProvider tenantProvider,
            [FromServices] CreateHandler handler,
            CancellationToken ct
            ) =>
        {
            var command = new ProdutoCreateCommand(
                tenantProvider.TenantId,
                req.Codigo,
                req.Descricao,
                req.Valor,
                req.Detalhes);
            
            var result = await handler.Handle(command, ct);

            return !result.IsSuccess 
                ? Results.BadRequest(new {error = result.Error}) 
                : Results.Created($"/{result.Value.Id}", result.Value);
        });
        
        return endpoints;
    }
}