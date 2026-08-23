using System.Net.Http.Json;
using OrcamentoSaaS.Shared.Dtos;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Extensions;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Client.Services;

public class FornecedorService(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("Api");
    
    public async Task<Result<FornecedorResponse>> CreateFornecedorAsync(FornecedorCreateRequest req)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/fornecedor", req);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<FornecedorResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<FornecedorResponse>();
        return Result<FornecedorResponse>.Success(data!);
    }
    
    public async Task<Result<FornecedorResponse>> BuscarFornecedorByIdAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"api/fornecedor/{id}", ct);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<FornecedorResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<FornecedorResponse>(cancellationToken: ct);
        return Result<FornecedorResponse>.Success(data!);
    }
    
    public async Task<PaginacaoResponse<FornecedorResponse>?> BuscarFornecedoresAsync(FornecedorQuery query, CancellationToken ct)
    {
        var url = $"api/fornecedor" +
                  $"?pagina={query.Pagina}" +
                  $"&tamanhoPagina={query.TamanhoPagina}" +
                  $"&busca={Uri.EscapeDataString(query.Busca ?? string.Empty)}";
        
        return await _httpClient.GetFromJsonAsync<
                   PaginacaoResponse<FornecedorResponse>>(url , ct) 
               ?? new PaginacaoResponse<FornecedorResponse>();
    }
    
    public async Task<Result<FornecedorDetalhesResponse>> BuscarFornecedorDetalhesAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"api/fornecedor/detalhes/{id}", ct);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<FornecedorDetalhesResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<FornecedorDetalhesResponse>(cancellationToken: ct);
        
        return Result<FornecedorDetalhesResponse>.Success(data!);
    }
}