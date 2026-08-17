using OrcamentoSaaS.Api.Features.Fornecedores.Commands;
using OrcamentoSaaS.Api.Features.Fornecedores.Queries;
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
            CreateFornecedorHandler fornecedorHandler,
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

            var result = await fornecedorHandler.Handle(command, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.Created($"/{result.Value.Id}", result.Value);
        });

        group.MapPut("/{id:guid}", async (
            Guid id,
            FornecedorEditRequest req,
            ITenantProvider tenantProvider,
            EditFornecedorHandler fornecedorHandler,
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

            var result = await fornecedorHandler.Handle(command, ct);

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });

        group.MapDelete("/{id:guid}", async (
            Guid id,
            DeleteFornecedorHandler fornecedorHandler,
            CancellationToken ct) =>
        {
            var result = await fornecedorHandler.Handle(id, ct);
            
            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.NoContent();
        });
        
        group.MapGet("/", async (
            [AsParameters] FornecedorQuery query,
            GetBuscarFornecedoresHandler fornecedoresHandler,
            CancellationToken ct) =>
        {
            var result = await fornecedoresHandler.Handle(query, ct);

            return !result.IsSuccess
                ? Results.NotFound(result.Error)
                : Results.Ok(result.Value);
        });

        return endpoints;
    }
}