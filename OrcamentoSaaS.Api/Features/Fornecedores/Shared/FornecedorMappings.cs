using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Shared;

public static class FornecedorMappings
{
    public static Result<Fornecedor> ToEntity(this FornecedorCreateCommand cmd, Documento documento)
    {
        return Fornecedor.Criar(
            cmd.TenantId,
            cmd.Nome,
            cmd.Cidade,
            cmd.Uf,
            cmd.Email,
            cmd.Telefone,
            documento,
            cmd.PorcentagemAVista,
            cmd.PorcentagemAPrazo);
    }
}