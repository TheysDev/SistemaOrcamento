using OrcamentoSaaS.Api.Features.Produtos.Commands;
using OrcamentoSaaS.Api.Features.Produtos.Queries;
using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Api.Features.Produtos;

public static class ProdutoEndPoint
{
    public static IEndpointRouteBuilder MapProduto(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/produto");
        
        group.MapPost("/", async (
            ProdutoCreateRequest req,
            ITenantProvider tenantProvider,
            CreateProdutoHandler produtoHandler,
            CancellationToken ct
            ) =>
        {
            var command = new ProdutoCreateCommand(
                tenantProvider.TenantId,
                req.Codigo,
                req.Descricao,
                req.Valor,
                req.Detalhes);
            
            var result = await produtoHandler.Handle(command, ct);

            return !result.IsSuccess 
                ? Results.BadRequest(new {error = result.Error}) 
                : Results.Created($"/{result.Value.Id}", result.Value);
        });

        group.MapPut("/{id:guid}", async (
            Guid id,
            ProdutoEditRequest req,
            EditProdutoHandler produtoHandler,
            ITenantProvider tenantProvider,
            CancellationToken ct) =>
        {
            var command = new ProdutoEditCommand(
                id,
                tenantProvider.TenantId,
                req.Descricao,
                req.Valor,
                req.Detalhes);

            var result = await produtoHandler.Handle(command, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });

        group.MapDelete("/{id:guid}", async (
            Guid id,
            DeleteProdutoHandler produtoHandler,
            CancellationToken ct
        ) =>
        {
            var result = await produtoHandler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });
        
        group.MapGet("/", async (
            [AsParameters] ProdutoQuery query,
            GetBuscarProdutosHandler produtoHandler,
            CancellationToken ct) =>
        {
            var result = await produtoHandler.Handle(query, ct);

            return !result.IsSuccess
                ? Results.NotFound(result.Error)
                : Results.Ok(result.Value);
        });
        
        group.MapGet("/{id:guid}", async (
            Guid id,
            GetProdutoByIdHandler produtoHandler,
            CancellationToken ct) =>
        {
            var result = await produtoHandler.Handle(id, ct);

            return !result.IsSuccess
                ? Results.NotFound(result.Error)
                : Results.Ok(result.Value);
        });
        
        return endpoints;
    }
}