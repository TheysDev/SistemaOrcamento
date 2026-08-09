using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Shared;

public static class FornecedorResponseMappings
{
    public static FornecedorResponse ToResponse(this Fornecedor fornecedor)
    {
        return new FornecedorResponse(
            fornecedor.Id,
            fornecedor.Nome,
            fornecedor.Cidade,
            fornecedor.Uf,
            fornecedor.Email,
            fornecedor.Telefone,
            fornecedor.Documento,
            fornecedor.PorcentagemAVista,
            fornecedor.PorcentagemAPrazo);
    }

    public static FornecedorResumoResponse ToResumoResponse(this Fornecedor fornecedor)
    {
        return new FornecedorResumoResponse(
            fornecedor.Id,
            fornecedor.Nome);
    }
}
