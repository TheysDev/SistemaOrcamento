using OrcamentoSaaS.Api.Features.Produtos.Commands;

namespace OrcamentoSaaS.Api.Features.Produtos.Shared;

public static class ProdutoBuilderExtension
{
    public static WebApplicationBuilder AddProduto(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateHandler>();
        builder.Services.AddScoped<EditHandler>();
        builder.Services.AddScoped<DeleteHandler>();
        
        return builder;
    }
}