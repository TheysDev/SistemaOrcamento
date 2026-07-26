using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrcamentoSaaS.Client;
using OrcamentoSaaS.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var apiUrl = builder.Configuration["ApiUrl"];

builder.Services.AddHttpClient(
    "Api",
    client =>
    {
        client.BaseAddress = new Uri(apiUrl!);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

builder.Services.AddScoped<ClienteService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
