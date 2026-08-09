using OrcamentoSaaS.Api.Features.Orcamentos.Commands;
using OrcamentoSaaS.Api.Features.Orcamentos.Queries;

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
        builder.Services.AddScoped<RejeitarHandler>();
        builder.Services.AddScoped<CancelarHandler>();

        builder.Services.AddScoped<GetByIdHandler>();
        
        return builder;
    }
}