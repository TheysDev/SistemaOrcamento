using OrcamentoSaaS.Api.Features.Clientes.Create;

namespace OrcamentoSaaS.Api.Features.Clientes.Shared;

public static class ClienteBuilderExtension
{
    public static WebApplicationBuilder AddCliente(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ClienteCreateHandler>();
        
        return builder;
    }
}