using OrcamentoSaaS.Shared.Dtos.Clientes;

namespace OrcamentoSaaS.Api.Features.Clientes.Shared;

public static class ClienteMappings
{
    public static Cliente ToEntity(this ClienteCreateCommand cmd, Documento documento)
    {
        return new Cliente(
            cmd.TenantId,
            cmd.Nome,
            cmd.Cidade,
            cmd.Uf,
            cmd.Email,
            cmd.Telefone,
            documento);
    }
}