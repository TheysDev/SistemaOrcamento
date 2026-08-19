using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Clientes;

public sealed record ClienteEditRequest : ClienteBaseRequest
{
    public Guid Id { get; init; }
}
    