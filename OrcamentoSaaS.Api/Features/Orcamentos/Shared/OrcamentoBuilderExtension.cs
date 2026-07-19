using OrcamentoSaaS.Api.Features.Orcamentos.Create;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Shared;

public static class OrcamentoBuilderExtension
{
    public static WebApplicationBuilder AddOrcamento(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<OrcamentoCreateHandler>();
        
        return builder;
    }
}