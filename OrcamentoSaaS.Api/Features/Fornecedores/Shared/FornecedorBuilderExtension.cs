using OrcamentoSaaS.Api.Features.Fornecedores.Commands;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Shared;

public static class FornecedorBuilderExtension
{
    public static WebApplicationBuilder AddFornecedor(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateHandler>();
        builder.Services.AddScoped<EditHandler>();
        builder.Services.AddScoped<DeleteHandler>();
        
        return builder;
    }
}