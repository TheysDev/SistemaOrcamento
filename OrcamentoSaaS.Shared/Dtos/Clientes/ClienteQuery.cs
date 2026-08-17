namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public sealed record ClienteQuery(
    string? Busca,
    int Pagina = 1,
    int TamanhoPagina = 10);