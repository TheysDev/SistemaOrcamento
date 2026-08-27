using System.Net.Http.Json;
using OrcamentoSaaS.Shared.Dtos;
using OrcamentoSaaS.Shared.Dtos.Produtos;
using OrcamentoSaaS.Shared.Extensions;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Client.Services;

public class ProdutoService(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("Api");
    
    public async Task<Result<ProdutoResponse>> CreateProdutoAsync(ProdutoCreateRequest req)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/produto", req);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<ProdutoResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<ProdutoResponse>();
        return Result<ProdutoResponse>.Success(data!);
    }
    
    public async Task<PaginacaoResponse<ProdutoResponse>?> BuscarProdutosAsync(ProdutoQuery query, CancellationToken ct)
    {
        var url = $"api/produto" +
                  $"?pagina={query.Pagina}" +
                  $"&tamanhoPagina={query.TamanhoPagina}" +
                  $"&busca={Uri.EscapeDataString(query.Busca ?? string.Empty)}";
        
        return await _httpClient.GetFromJsonAsync<
                   PaginacaoResponse<ProdutoResponse>>(url , ct) 
               ?? new PaginacaoResponse<ProdutoResponse>();
    }
    
    public async Task<Result> DeleteProdutoAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync($"api/produto/{id}", ct);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<ProdutoResponse>.Fail(errorMessage);

    }
    
    public async Task<Result<ProdutoResponse>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"api/produto/{id}", ct);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<ProdutoResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<ProdutoResponse>(cancellationToken: ct);
        return Result<ProdutoResponse>.Success(data!);
    }
    
    public async Task<Result> EditarProdutoAsync(Guid id, ProdutoEditRequest req)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/produto/{id}", req);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<ProdutoResponse>.Fail(errorMessage);

    }
}