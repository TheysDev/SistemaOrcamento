namespace OrcamentoSaaS.Api.Utils;

public static class PaginacaoExtensions
{
    public static async Task<PaginacaoResponse<T>> PaginarAsync<T>(this IQueryable<T> source, int pagina, 
        int tamanhoPagina, CancellationToken ct)
    {
        var totalItens = await source.CountAsync(ct);

        var itens = await source
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync(ct);

        return new PaginacaoResponse<T>
        {
            Itens = itens,
            Pagina = pagina,
            TamanhoPagina = tamanhoPagina,
            TotalItens = totalItens,
            TotalPaginas = (int)Math.Ceiling(
                totalItens / (double)tamanhoPagina)
        };

    }
}