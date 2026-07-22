using OrcamentoSaaS.Api.Features.Orcamentos.Commands;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Shared;

public static class OrcamentoBuilderExtension
{
    public static WebApplicationBuilder AddOrcamento(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateHandler>();
        
        return builder;
    }
}