using OrcamentoSaaS.Api.Features.Fornecedores.Shared;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Commands;

public class CreateFornecedorHandler(AppDbContext db)
{
    public async Task<Result<FornecedorResponse>> Handle(
        FornecedorCreateCommand cmd, 
        CancellationToken ct)
    {
        var resultado = Documento.Criar(cmd.Documento);

        if (resultado.IsFailure)
            return Result<FornecedorResponse>.Fail(resultado.Error);
            
        var documento = resultado.Value;
            
        var fornecedor = await db.Fornecedores
            .IgnoreQueryFilters(["SoftDelete"])
            .FirstOrDefaultAsync(f =>
                f.Documento == documento, cancellationToken: ct);

        if (fornecedor is not null)
        {
            if(fornecedor.IsActive)
                return Result<FornecedorResponse>.Fail("Já existe Fornecedor cadastrado com esse CPF/CNPJ");
            
            fornecedor.Ativar();
            fornecedor.EditarDados(
                cmd.Nome,
                cmd.Cidade,
                cmd.Uf,
                cmd.Email,
                cmd.Telefone);
        }
        else
        {
            var result = cmd.ToEntity(documento);
        
            if (result.IsFailure)
                return Result<FornecedorResponse>.Fail(result.Error);
        
            fornecedor = result.Value;
            
            db.Fornecedores.Add(fornecedor);
        }
        
        await db.SaveChangesAsync(ct);

        return Result<FornecedorResponse>.Success(fornecedor.ToResponse());
    }
}