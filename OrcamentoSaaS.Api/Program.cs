using System.Globalization;
using Microsoft.AspNetCore.Localization;
using OrcamentoSaaS.Api.Features.Clientes;
using OrcamentoSaaS.Api.Features.Clientes.Shared;
using OrcamentoSaaS.Api.Features.Fornecedores;
using OrcamentoSaaS.Api.Features.Fornecedores.Shared;
using OrcamentoSaaS.Api.Features.Orcamentos;
using OrcamentoSaaS.Api.Features.Orcamentos.Shared;
using OrcamentoSaaS.Api.Features.Produtos;
using OrcamentoSaaS.Api.Features.Produtos.Shared;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string" + "'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy",
    policy => policy
        .WithOrigins("https://localhost:7132", "https://localhost:7247")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()));

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

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();   
app.UseAuthorization();  

app.MapIdentityApi<AppUser>();

//EndPoints
var api = app.MapGroup("/api");

api.MapCliente().MapFornecedor().MapOrcamento().MapProduto();


var culture = new CultureInfo("pt-BR");

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = [culture],
    SupportedUICultures = [culture]
};

app.UseRequestLocalization(localizationOptions);

app.Run();

