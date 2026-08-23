using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Shared;

public static class FornecedorResponseMappings
{
    extension(Fornecedor fornecedor)
    {
        public FornecedorResponse ToResponse()
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

        public FornecedorResumoResponse ToResumoResponse()
        {
            return new FornecedorResumoResponse(
                fornecedor.Id,
                fornecedor.Nome);
        }
    }
}
