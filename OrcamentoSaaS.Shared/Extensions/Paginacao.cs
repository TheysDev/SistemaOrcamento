namespace OrcamentoSaaS.Shared.Extensions;

public record PaginacaoResponse<T>
{
    public IEnumerable<T> Itens { get; init; } = [];
    public int Pagina { get; init; }
    public int TamanhoPagina { get; init; }
    public int TotalItens { get; init; }
    public int TotalPaginas { get; init; }
    
    public bool ProximaPagina => Pagina < TotalPaginas;
    public bool PaginaAnterior => Pagina > 1;

    public PaginacaoResponse(IList<T> itens, int pagina, int tamanhoPagina, int totalItens)
    {
        Itens = itens;
        Pagina = pagina;
        TamanhoPagina = tamanhoPagina;
        TotalItens = totalItens;
        TotalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina);
    }
    
}