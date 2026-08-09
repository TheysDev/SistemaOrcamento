using OrcamentoSaaS.Api.Features.Fornecedores.Commands;
using OrcamentoSaaS.Api.Features.Fornecedores.Queries;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Shared;

public static class FornecedorBuilderExtension
{
    public static WebApplicationBuilder AddFornecedor(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateFornecedorHandler>();
        builder.Services.AddScoped<EditFornecedorHandler>();
        builder.Services.AddScoped<DeleteFornecedorHandler>();
        
        builder.Services.AddScoped<GetAllFornecedoresHandler>();
        
        return builder;
    }
}