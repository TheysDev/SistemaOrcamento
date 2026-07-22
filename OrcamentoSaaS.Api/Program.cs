using System.Globalization;
using Microsoft.AspNetCore.Localization;
using OrcamentoSaaS.Api.Features.Clientes.Create;
using OrcamentoSaaS.Api.Features.Clientes.Edit;
using OrcamentoSaaS.Api.Features.Clientes.Shared;
using OrcamentoSaaS.Api.Features.Fornecedores.Create;
using OrcamentoSaaS.Api.Features.Fornecedores.Shared;
using OrcamentoSaaS.Api.Features.Orcamentos.Create;
using OrcamentoSaaS.Api.Features.Orcamentos.Shared;
using OrcamentoSaaS.Api.Features.Produtos.Create;
using OrcamentoSaaS.Api.Features.Produtos.Shared;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string" + "'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddValidation();

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ITenantProvider, TenantProvider>();

//DI Create
builder.AddCliente().AddFornecedor().AddProduto().AddOrcamento();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();   
app.UseAuthorization();  

app.MapIdentityApi<AppUser>();

//EndPoints
//Create
app.ProdutoCreateRoute();
app.CreateClienteRoute();
app.FornecedorCreateRoute();
app.OrcamentoCreateRoute();

//Edit
app.EditClienteRoute();

var culture = new CultureInfo("pt-BR");

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = [culture],
    SupportedUICultures = [culture]
};

app.UseRequestLocalization(localizationOptions);

app.Run();

