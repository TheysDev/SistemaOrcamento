using OrcamentoSaaS.Api.Features.Fornecedores.Shared;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Commands;

public class EditFornecedorHandler(AppDbContext db)
{
    public async Task<Result<FornecedorResponse>> Handle(
        FornecedorEditCommand cmd,
        CancellationToken ct)
    {
        var fornecedor = await db.Fornecedores.FindAsync([cmd.Id], ct);
        
        if(fornecedor is null)
            return Result<FornecedorResponse>.Fail("Fornecedor não encontrado!");

        var result = fornecedor.EditarDados(cmd.Nome, cmd.Cidade, cmd.Uf, cmd.Email, cmd.Telefone);

        if (result.IsFailure)
            return Result<FornecedorResponse>.Fail(result.Error);
        
        await  db.SaveChangesAsync(ct);

        var response = fornecedor.ToResponse();
        
        return Result<FornecedorResponse>.Success(response);
    }
}