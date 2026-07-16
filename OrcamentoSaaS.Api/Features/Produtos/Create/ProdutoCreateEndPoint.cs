using OrcamentoSaaS.Shared.Dtos.Produtos;

namespace OrcamentoSaaS.Api.Features.Produtos.Create;

public static class ProdutoCreateEndPoint
{
    public static void ProdutoCreateRoute(this WebApplication app)
    {
        app.MapPost("/produto/cadastrar", async (
            ProdutoCreateRequest req,
            ITenantProvider tenantProvider,
            ProdutoCreateHandler handler,
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
                : Results.Created($"produto/{result.Value.Id}", result.Value);
        });
    }
}