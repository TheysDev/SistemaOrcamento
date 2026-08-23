using OrcamentoSaaS.Api.Features.Fornecedores.Shared;
using OrcamentoSaaS.Shared.Dtos.Fornecedores;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Dtos.Pedidos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Fornecedores.Queries;

public class GetFornecedorDetalhesHandler(AppDbContext db)
{
    public async Task<Result<FornecedorDetalhesResponse>> Handle(Guid id, CancellationToken ct)
    {
        var fornecedorResumo = await db.Fornecedores.AsNoTracking()
            .Where(f => f.Id == id)
            .Select(f => new
            {
                Fornecedor = f,
                TotalOrcamentos = db.Orcamentos.Count(o => o.FornecedorId == id),
                TotalAprovados = db.Orcamentos.Count(o => o.FornecedorId == id && o.Status == StatusOrcamento.Aprovado),
                TotalRejeitados =
                    db.Orcamentos.Count(o => o.FornecedorId == id && o.Status == StatusOrcamento.Rejeitado),
                TotalCancelados =
                    db.Orcamentos.Count(o => o.FornecedorId == id && o.Status == StatusOrcamento.Cancelado),
                TotalPedidos = db.Pedidos.Count(p => p.Orcamento.FornecedorId == id),
                PedidosValorTotal = db.Pedidos
                    .Where(p => p.Orcamento.FornecedorId == id)
                    .Sum(p => (decimal?)p.ValorTotal) ?? 0m
            })
            .FirstOrDefaultAsync(ct);
        
        if(fornecedorResumo is null)
            return Result<FornecedorDetalhesResponse>.Fail("Fornecedor não Encontrado");

        var fornecedorDto =
            new FornecedorResumoResponse(fornecedorResumo.Fornecedor.Id, fornecedorResumo.Fornecedor.Nome);
        
        var ultimosOrcamentos = await db.Orcamentos.AsNoTracking()
            .Where(o => o.FornecedorId == id)
            .OrderByDescending(o => o.Codigo)
            .Take(5)
            .Select(o => new OrcamentoResponse(
                o.Id,
                new ClienteResumoResponse(o.ClienteId, o.Cliente.Nome),
                fornecedorDto,
                o.CodigoFormatado,
                o.Validade,
                o.Status))
            .ToListAsync(ct);
        
        var ultimosPedidos = await db.Pedidos.AsNoTracking()
            .Where(p => p.Orcamento.ClienteId == id)
            .OrderByDescending(p => p.Codigo)
            .Take(5)
            .Select(p => new PedidosResponse(
                p.Id,
                new ClienteResumoResponse(p.Orcamento.ClienteId, p.Orcamento.Cliente.Nome),
                fornecedorDto,
                p.CodigoFormatado,
                p.Data,
                p.ValorTotal,
                p.Status))
            .ToListAsync(ct);
        
        var response = new FornecedorDetalhesResponse(
            Id: fornecedorResumo.Fornecedor.Id,
            Fornecedor: fornecedorResumo.Fornecedor.ToResponse(),
            Orcamentos: ultimosOrcamentos,
            Pedidos: ultimosPedidos,
            PorcentagemAVista: fornecedorResumo.Fornecedor.PorcentagemAVista,
            PorcentagemAPrazo: fornecedorResumo.Fornecedor.PorcentagemAPrazo,
            TotalOrcamentos: fornecedorResumo.TotalOrcamentos,
            TotalOrcamentosAprovados: fornecedorResumo.TotalAprovados,
            TotalOrcamentosRejeitados: fornecedorResumo.TotalRejeitados,
            TotalOrcamentosCancelados: fornecedorResumo.TotalCancelados,
            TotalPedidos: fornecedorResumo.TotalPedidos,
            PedidosValorTotal: fornecedorResumo.PedidosValorTotal
        );
        
        return Result<FornecedorDetalhesResponse>.Success(response);
    }
}