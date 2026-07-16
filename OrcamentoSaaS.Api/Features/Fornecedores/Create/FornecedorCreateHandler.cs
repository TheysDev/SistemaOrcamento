using OrcamentoSaaS.Api.Features.Fornecedores.Shared;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Create;

public class FornecedorCreateHandler(AppDbContext db)
{
    public async Task<Result<FornecedorResponse>> Handle(
        FornecedorCreateCommand cmd, 
        CancellationToken ct)
    {
        var resultado = Documento.Criar(cmd.Documento);

        if (resultado.IsFailure)
            return Result<FornecedorResponse>.Fail(resultado.Error);
            
        var documento = resultado.Value;
            
        var existe = await db.Fornecedores.AnyAsync(f => f.Documento == documento, cancellationToken: ct);
            
        if (existe)
            return Result<FornecedorResponse>.Fail("Já existe Fornecedor cadastrado com esse CPF/CNPJ");
        
        var result = cmd.ToEntity(documento);
        
        if (result.IsFailure)
            Result<FornecedorResponse>.Fail(result.Error);
        
        var fornecedor = result.Value;
            
        db.Fornecedores.Add(fornecedor);
        
        await db.SaveChangesAsync(ct); 
        
        return Result<FornecedorResponse>.Success(fornecedor.ToResponse());
    }
}