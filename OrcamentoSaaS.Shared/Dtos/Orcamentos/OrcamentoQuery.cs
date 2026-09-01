namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public sealed record OrcamentoQuery(
    string? Busca,
    int Pagina = 1,
    int TamanhoPagina = 10);