using OrcamentoSaaS.Api.Features.Clientes.Shared;
using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Commands;

public class CreateHandler(AppDbContext db)
{
    public async Task<Result<ClienteResponse>> Handle(
        ClienteCreateCommand cmd, 
        CancellationToken ct)
    {
        var resultado = Documento.Criar(cmd.Documento);

        if (resultado.IsFailure) 
            return Result<ClienteResponse>.Fail(resultado.Error);
            
        var documento = resultado.Value;
        
        var cliente = await db.Clientes
            .IgnoreQueryFilters(["SoftDelete"])
            .FirstOrDefaultAsync(c => 
                c.TenantId == cmd.TenantId && 
                c.Documento == documento, cancellationToken: ct);

        if (cliente is not null)
        {
            if (cliente.IsActive) 
                return Result<ClienteResponse>.Fail("Já existe Cliente cadastrado com esse CPF/CNPJ");
            
            cliente.Ativar();
            cliente.EditarDados(
                cmd.Nome,
                cmd.Cidade,
                cmd.Uf,
                cmd.Email,
                cmd.Telefone);
        }
        else
        {
            cliente = cmd.ToEntity(documento);
            db.Clientes.Add(cliente);
        }
        
        await db.SaveChangesAsync(ct);
        
        return Result<ClienteResponse>.Success(cliente.ToResponse());
    }
} 
