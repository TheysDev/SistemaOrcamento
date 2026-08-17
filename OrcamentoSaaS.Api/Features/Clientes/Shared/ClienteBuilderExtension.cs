using OrcamentoSaaS.Api.Features.Clientes.Commands;
using OrcamentoSaaS.Api.Features.Clientes.Queries;

namespace OrcamentoSaaS.Api.Features.Clientes.Shared;

public static class ClienteBuilderExtension
{
    public static WebApplicationBuilder AddCliente(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateClienteHandler>();
        builder.Services.AddScoped<EditClienteHandler>();
        builder.Services.AddScoped<DeleteClienteHandler>();

        builder.Services.AddScoped<GetBuscarClientesHandler>();
        builder.Services.AddScoped<GetClienteDetalhesHandler>();
        
        return builder;
    }
}