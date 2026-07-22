using OrcamentoSaaS.Api.Features.Clientes.Create;
using OrcamentoSaaS.Api.Features.Clientes.Edit;

namespace OrcamentoSaaS.Api.Features.Clientes.Shared;

public static class ClienteBuilderExtension
{
    public static WebApplicationBuilder AddCliente(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ClienteCreateHandler>();
        builder.Services.AddScoped<ClienteEditHandler>();
        
        return builder;
    }
}