using System.Net.Http.Json;
using OrcamentoSaaS.Shared.Dtos.Clientes;

namespace OrcamentoSaaS.Client.Services;

public class ClienteService(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("Api");

    public async Task CreateClienteAsync(ClienteCreateRequest req)
    {
        await _httpClient.PostAsJsonAsync($"api/cliente", req);
    }
}