using OrcamentoSaaS.Api.Features.Fornecedores.Commands;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;

namespace OrcamentoSaaS.Api.Features.Fornecedores;

public static class FornecedorEndPoints
{
    public static IEndpointRouteBuilder MapFornecedor(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/fornecedor");
        
        group.MapPost("/", async (
            FornecedorCreateRequest req,
            ITenantProvider tenantProvider,
            CreateHandler handler,
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

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.Created($"/{result.Value.Id}", result.Value);
        });

        group.MapPut("/{id:guid}", async (
            Guid id,
            FornecedorEditRequest req,
            ITenantProvider tenantProvider,
            EditHandler handler,
            CancellationToken ct) =>
        {
            var command = new FornecedorEditCommand(
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