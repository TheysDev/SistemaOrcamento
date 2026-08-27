using OrcamentoSaaS.Api.Features.Produtos.Commands;
using OrcamentoSaaS.Api.Features.Produtos.Queries;

namespace OrcamentoSaaS.Api.Features.Produtos.Shared;

public static class ProdutoBuilderExtension
{
    public static WebApplicationBuilder AddProduto(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateProdutoHandler>();
        builder.Services.AddScoped<EditProdutoHandler>();
        builder.Services.AddScoped<DeleteProdutoHandler>();

        builder.Services.AddScoped<GetBuscarProdutosHandler>();
        builder.Services.AddScoped<GetProdutoByIdHandler>();
        
        return builder;
    }
}