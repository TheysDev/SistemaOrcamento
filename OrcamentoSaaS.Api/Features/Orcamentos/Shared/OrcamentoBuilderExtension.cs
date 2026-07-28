using OrcamentoSaaS.Api.Features.Orcamentos.Commands;

namespace OrcamentoSaaS.Api.Features.Orcamentos.Shared;

public static class OrcamentoBuilderExtension
{
    public static WebApplicationBuilder AddOrcamento(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateHandler>();
        builder.Services.AddScoped<EditHandler>();
        builder.Services.AddScoped<DeleteHandler>();
        builder.Services.AddScoped<EnviarHandler>();
        builder.Services.AddScoped<AprovarHandler>();
        
        return builder;
    }
}