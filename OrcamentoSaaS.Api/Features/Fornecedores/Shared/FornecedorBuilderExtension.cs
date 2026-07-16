using OrcamentoSaaS.Api.Features.Fornecedores.Create;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Shared;

public static class FornecedorBuilderExtension
{
    public static WebApplicationBuilder AddFornecedor(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<FornecedorCreateHandler>();
        
        return builder;
    }
}