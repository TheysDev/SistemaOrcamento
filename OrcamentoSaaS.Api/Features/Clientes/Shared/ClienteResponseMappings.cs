using OrcamentoSaaS.Shared.Dtos.Clientes;

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
}