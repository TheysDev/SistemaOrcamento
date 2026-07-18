using OrcamentoSaaS.Api.Features.Clientes.Shared;
using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Create;

public class ClienteCreateHandler(AppDbContext db)
{
    public async Task<Result<ClienteResponse>> Handle(
        ClienteCreateCommand cmd, 
        CancellationToken ct)
    {
        var resultado = Documento.Criar(cmd.Documento);

        if (resultado.IsFailure) 
            return Result<ClienteResponse>.Fail(resultado.Error);
            
        var documento = resultado.Value;
            
        var existe = await db.Clientes.AsNoTracking().AnyAsync(f => 
                f.Documento == documento, cancellationToken: ct);
            
        if (existe)
            return Result<ClienteResponse>.Fail("Já existe Cliente cadastrado com esse CPF/CNPJ");

        var cliente = cmd.ToEntity(documento);
            
        db.Clientes.Add(cliente);
        await db.SaveChangesAsync(ct);
            
        return Result<ClienteResponse>.Success(cliente.ToResponse());
    }
} 
