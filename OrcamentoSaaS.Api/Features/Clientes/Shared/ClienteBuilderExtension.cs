using OrcamentoSaaS.Api.Features.Clientes.Commands;

namespace OrcamentoSaaS.Api.Features.Clientes.Shared;

public static class ClienteBuilderExtension
{
    public static WebApplicationBuilder AddCliente(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateHandler>();
        builder.Services.AddScoped<EditHandler>();
        builder.Services.AddScoped<DeleteHandler>();
        
        return builder;
    }
}