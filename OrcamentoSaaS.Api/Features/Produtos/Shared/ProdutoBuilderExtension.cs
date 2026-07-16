using OrcamentoSaaS.Api.Features.Produtos.Create;

namespace OrcamentoSaaS.Api.Features.Produtos.Shared;

public static class ProdutoBuilderExtension
{
    public static WebApplicationBuilder AddProduto(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ProdutoCreateHandler>();
        
        return builder;
    }
}