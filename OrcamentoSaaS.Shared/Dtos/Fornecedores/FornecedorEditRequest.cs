using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Fornecedores;

public record FornecedorEditRequest : FornecedorBaseRequest
{
    public Guid Id { get; init; }
}