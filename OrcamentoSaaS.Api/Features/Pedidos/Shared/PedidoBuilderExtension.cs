using OrcamentoSaaS.Api.Features.Pedidos.Commands;

namespace OrcamentoSaaS.Api.Features.Pedidos.Shared;

public static class PedidoBuilderExtension
{
    public static WebApplicationBuilder AddPedido(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<PagarParcelaHandler>();
        
        return builder;
    }
}