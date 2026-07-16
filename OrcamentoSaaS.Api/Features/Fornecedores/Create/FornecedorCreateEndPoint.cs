using OrcamentoSaaS.Shared.Dtos.Fornecedores;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Create;

public static class FornecedorCreateEndPoint
{
    public static void FornecedorCreateRoute(this WebApplication app)
    {
        app.MapPost("/fornecedor/cadastrar", async (
            FornecedorCreateRequest req,
            ITenantProvider tenantProvider,
            FornecedorCreateHandler handler,
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
                ? Results.Created($"fornecedor/{result.Value.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        });
    }
}