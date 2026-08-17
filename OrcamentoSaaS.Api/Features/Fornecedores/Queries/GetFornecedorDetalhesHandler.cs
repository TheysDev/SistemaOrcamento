using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Queries;

public class GetFornecedorDetalhesHandler(AppDbContext db)
{
    public async Task<Result<FornecedorDetalhesResponse>> Handle(Guid id, CancellationToken ct)
    {
        var response = await db.Fornecedores
            .AsNoTracking()
            .Where(f => f.Id == id)
            .Select(f => new FornecedorDetalhesResponse(
                f.Id,
                f.Nome,
                f.Cidade,
                f.Uf,
                f.Email,
                f.Telefone,
                f.Documento,
                f.PorcentagemAVista,
                f.PorcentagemAPrazo))
            .FirstOrDefaultAsync(ct);
        
        return response is null 
            ? Result<FornecedorDetalhesResponse>.Fail("Fornecedor não encontrado") 
            : Result<FornecedorDetalhesResponse>.Success(response);
    }
}