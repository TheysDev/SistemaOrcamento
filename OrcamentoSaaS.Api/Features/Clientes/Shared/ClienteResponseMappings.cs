using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;

namespace OrcamentoSaaS.Api.Features.Clientes.Shared;

public static class ClienteResponseMappings
{
    public static ClienteResponse ToResponse(this Cliente cliente)
    {
        return new ClienteResponse(
            cliente.Id,
            cliente.Nome,
            cliente.Cidade,
            cliente.Uf,
            cliente.Email,
            cliente.Telefone,
            cliente.Documento);
    }

    public static ClienteResumoResponse ToResumoResponse(this Cliente cliente)
    {
        return new ClienteResumoResponse(
            cliente.Id,
            cliente.Nome);
    }
}