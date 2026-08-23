using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;

namespace OrcamentoSaaS.Api.Features.Clientes.Shared;

public static class ClienteResponseMappings
{
    extension(Cliente cliente)
    {
        public ClienteResponse ToResponse()
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

        public ClienteResumoResponse ToResumoResponse()
        {
            return new ClienteResumoResponse(
                cliente.Id,
                cliente.Nome);
        }
    }
}