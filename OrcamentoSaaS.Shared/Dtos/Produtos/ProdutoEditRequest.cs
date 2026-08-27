
namespace OrcamentoSaaS.Shared.Dtos.Produtos;

public record ProdutoEditRequest : ProdutoBaseRequest
{
    public Guid Id { get; init; }
}
    