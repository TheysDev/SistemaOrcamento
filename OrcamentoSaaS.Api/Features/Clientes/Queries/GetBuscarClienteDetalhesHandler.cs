using OrcamentoSaaS.Api.Features.Clientes.Shared;
using OrcamentoSaaS.Shared.Dtos.Clientes;
using OrcamentoSaaS.Shared.Dtos.Orcamentos;
using OrcamentoSaaS.Shared.Dtos.Pedidos;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Features.Clientes.Queries;

public class GetBuscarClienteDetalhesHandler(AppDbContext db)
{
    public async Task<Result<ClienteDetalhesResponse>> Handle(Guid id, CancellationToken ct)
    {
        var clienteResumo = await db.Clientes.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new
            {
                Cliente = c,
                TotalOrcamentos = db.Orcamentos.Count(o => o.ClienteId == id),
                TotalAprovados = db.Orcamentos.Count(o => o.ClienteId == id && o.Status == StatusOrcamento.Aprovado),
                TotalRejeitados = db.Orcamentos.Count(o => o.ClienteId == id && o.Status == StatusOrcamento.Rejeitado),
                TotalCancelados = db.Orcamentos.Count(o => o.ClienteId == id && o.Status == StatusOrcamento.Cancelado),
                TotalPedidos = db.Pedidos.Count(p => p.Orcamento.ClienteId == id),
                PedidosValorTotal = db.Pedidos
                    .Where(p => p.Orcamento.ClienteId == id)
                    .Sum(p => (decimal?)p.ValorTotal) ?? 0m
            })
            .FirstOrDefaultAsync(ct);
        
        if (clienteResumo is null)
            return Result<ClienteDetalhesResponse>.Fail("Cliente não encontrado");

        var clienteDto = new ClienteResumoResponse(clienteResumo.Cliente.Id, clienteResumo.Cliente.Nome);

        var ultimosOrcamentos = await db.Orcamentos.AsNoTracking()
            .Where(o => o.ClienteId == id)
            .OrderByDescending(o => o.Codigo)
            .Take(5)
            .Select(o => new OrcamentoResponse(
                o.Id,
                clienteDto,
                new FornecedorResumoResponse(o.FornecedorId, o.Fornecedor.Nome),
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
                clienteDto,
                new FornecedorResumoResponse(p.Orcamento.FornecedorId, p.Orcamento.Fornecedor.Nome),
                p.CodigoFormatado,
                p.Data,
                p.ValorTotal,
                p.Status))
            .ToListAsync(ct);
        
        var response = new ClienteDetalhesResponse(
            Id: clienteResumo.Cliente.Id,
            Clientes: clienteResumo.Cliente.ToResponse(),
            Orcamentos: ultimosOrcamentos,
            Pedidos: ultimosPedidos,
            TotalOrcamentos: clienteResumo.TotalOrcamentos,
            TotalOrcamentosAprovados: clienteResumo.TotalAprovados,
            TotalOrcamentosRejeitados: clienteResumo.TotalRejeitados,
            TotalOrcamentosCancelados: clienteResumo.TotalCancelados,
            TotalPedidos: clienteResumo.TotalPedidos,
            PedidosValorTotal: clienteResumo.PedidosValorTotal
        );
        
        return Result<ClienteDetalhesResponse>.Success(response);
    }
}