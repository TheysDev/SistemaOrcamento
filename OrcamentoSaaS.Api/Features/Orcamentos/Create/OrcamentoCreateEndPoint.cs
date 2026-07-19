using OrcamentoSaaS.Api.Features.Orcamentos.Create;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Create;

public static class OrcamentoCreateEndPoint
{
    public static void OrcamentoCreateRoute(this WebApplication app)
    {
        app.MapPost("/orcamento/cadastrar", async (
            OrcamentoRequest req, 
            ITenantProvider tenantProvider,
            OrcamentoCreateHandler handler,
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
                ? Results.Created($"orcamento/{result.Value.Id}", result.Value)
                : Results.BadRequest(new { error = result.Error });
        });
    }
}