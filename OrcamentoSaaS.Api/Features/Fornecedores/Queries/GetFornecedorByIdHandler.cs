using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Queries;

public class GetFornecedorByIdHandler(AppDbContext db)
{
    public async Task<Result<FornecedorResponse>> Handle(Guid id, CancellationToken ct)
    {
        var response = await db.Fornecedores
            .AsNoTracking()
            .Where(f => f.Id == id)
            .Select(f => new FornecedorResponse(
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
            ? Result<FornecedorResponse>.Fail("Fornecedor não encontrado") 
            : Result<FornecedorResponse>.Success(response);
    }
}