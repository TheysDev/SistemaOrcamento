using System.Net.Http.Json;
using OrcamentoSaaS.Shared.Dtos;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Extensions;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Client.Services;

public class OrcamentoService(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("Api");
    
    
    public async Task<PaginacaoResponse<OrcamentoResponse>?> BuscarOrcamentosAsync(OrcamentoQuery query, CancellationToken ct)
    {
        var url = $"api/orcamento" +
                  $"?pagina={query.Pagina}" +
                  $"&tamanhoPagina={query.TamanhoPagina}" +
                  $"&busca={Uri.EscapeDataString(query.Busca ?? string.Empty)}";
        
        return await _httpClient.GetFromJsonAsync<
                   PaginacaoResponse<OrcamentoResponse>>(url , ct) 
               ?? new PaginacaoResponse<OrcamentoResponse>();
    }
    
    public async Task<Result<OrcamentoResponse>> CreateOrcamentoAsync(OrcamentoCreateRequest req)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/orcamento", req);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<OrcamentoResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<OrcamentoResponse>();
        return Result<OrcamentoResponse>.Success(data!);
    }
    
    public async Task<Result<OrcamentoDetalhesResponse>> BuscarOrcamentoByIdAsync (Guid id, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"api/orcamento/{id}", ct);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
            var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

            return Result<OrcamentoDetalhesResponse>.Fail(errorMessage);
        }
        
        var data = await response.Content.ReadFromJsonAsync<OrcamentoDetalhesResponse>(cancellationToken: ct);
        return Result<OrcamentoDetalhesResponse>.Success(data!);
    }
    
    public async Task<Result> EditarOrcamentoAsync(Guid id, OrcamentoEditRequest req, CancellationToken ct)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/orcamento/{id}", req, cancellationToken: ct);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<OrcamentoDetalhesResponse>.Fail(errorMessage);

    }
    
    public async Task<Result> DeleteOrcamentoAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync($"api/cliente/{id}", ct);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<OrcamentoResponse>.Fail(errorMessage);

    }
    
    public async Task<Result> AprovarOrcamentoAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.PostAsync($"api/orcamento/aprovar/{id}", null, ct);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<OrcamentoResponse>.Fail(errorMessage);

    }

    public async Task<Result> EnviarOrcamentoAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.PostAsync($"api/orcamento/enviar/{id}", null, ct);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<OrcamentoResponse>.Fail(errorMessage);
    }

    public async Task<Result> RejeitarOrcamentoAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.PostAsync($"api/orcamento/rejeitar/{id}", null, ct);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<OrcamentoResponse>.Fail(errorMessage);
    }

    public async Task<Result> CancelarOrcamentoAsync(Guid id, CancellationToken ct)
    {
        var response = await _httpClient.PostAsync($"api/orcamento/cancelar/{id}", null, ct);

        if (response.IsSuccessStatusCode) 
            return Result.Success();
        
        var errorBody = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: ct);
        var errorMessage = errorBody?.Error ?? "Ocorreu um erro ao processar a requisição.";

        return Result<OrcamentoResponse>.Fail(errorMessage);
    }
}