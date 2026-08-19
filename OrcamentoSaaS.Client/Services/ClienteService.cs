using System.Net.Http.Json;
using OrcamentoSaaS.Shared.Dtos;
using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Extensions;
using OrcamentoSaaS.Shared.Results;


namespace OrcamentoSaaS.Client.Services;

public class ClienteService(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("Api");

    
    public async Task<Result<ClienteResponse>> CreateClienteAsync(ClienteCreateRequest req)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/cliente", req);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<ClienteResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<ClienteResponse>();
        return Result<ClienteResponse>.Success(data!);
    }

    public async Task<PaginacaoResponse<ClienteResponse>?> BuscarClientesAsync(ClienteQuery query, CancellationToken ct)
    {
        var url = $"api/cliente" +
                  $"?pagina={query.Pagina}" +
                  $"&tamanhoPagina={query.TamanhoPagina}" +
                  $"&busca={Uri.EscapeDataString(query.Busca ?? string.Empty)}";
        
       return await _httpClient.GetFromJsonAsync<
                  PaginacaoResponse<ClienteResponse>>(url , ct) 
              ?? new PaginacaoResponse<ClienteResponse>();
    }
    
    public async Task<bool> DeleteClienteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/cliente/{id}");

        return response.IsSuccessStatusCode;
    }

    public async Task<Result<ClienteResponse>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"api/cliente/{id}", ct);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<ClienteResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<ClienteResponse>(cancellationToken: ct);
        return Result<ClienteResponse>.Success(data!);
    }
    
    public async Task<Result> EditarClienteAsync(Guid id, ClienteEditRequest req)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/cliente/{id}", req);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<ClienteResponse>.Fail(errorMessage);

    }
}