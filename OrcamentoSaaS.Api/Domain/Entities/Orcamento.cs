using Microsoft.EntityFrameworkCore.Query.Internal;
using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Api.Domain.Entities;

public class Orcamento
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }

    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;
    
    public Guid FornecedorId { get; private set; }
    public Fornecedor Fornecedor { get; private set; } = null!;
    
    private readonly List<ItemOrcamento> _itens = [];
    public IReadOnlyList<ItemOrcamento> Itens => _itens.AsReadOnly();

    public int Codigo { get; init; }
    public DateOnly Validade { get; private set; }
    public int NumeroParcelas { get; private set; }
    public decimal Valor => _itens.Sum(i => i.Total);
    public decimal Desconto => _itens.Sum(i => i.DescontoItem);
    public StatusOrcamento Status { get; private set; }
    public string? Observacao { get; private set; } 
    public bool IsActive { get; private set; }
    
    public string CodigoFormatado => $"ORC - {Codigo:D6}";
    
    protected Orcamento()
    {}

    private Orcamento(Guid tenantId, Guid clienteId, Guid fornecedorId, int codigo, DateOnly validade, int parcelas, string? observacao)
    {
        TenantId = tenantId;
        ClienteId = clienteId;
        FornecedorId = fornecedorId;
        Codigo = codigo;
        Validade = validade;
        NumeroParcelas = parcelas;
        Status = StatusOrcamento.Rascunho;
        Observacao = observacao;
        IsActive = true;
    }

    public static Result<Orcamento> Criar(
        Guid tenantId, 
        Guid clienteId, 
        Guid fornecedorId,
        int codigo,
        DateOnly validade, 
        ICollection<ItemOrcamento> itens, 
        int parcelas,
        string? observacao)
    {
        if(clienteId == Guid.Empty)
            return Result<Orcamento>.Fail("O Cliente deve ser informado.");
        
        if(fornecedorId == Guid.Empty)
            return Result<Orcamento>.Fail("O Fornecedor deve ser informado.");
        
        var existeItens = itens.Count != 0;
            
        if (!existeItens)
            return Result<Orcamento>.Fail("O Orcamento deve ter ao menos um item.");

        var vencimento = DateOnly.FromDateTime(DateTime.Now.AddDays(7));

        if (validade < vencimento)
            return Result<Orcamento>.Fail("A validade deve ser de uma semana ou mais");
        
        var orcamento = new Orcamento(tenantId, clienteId, fornecedorId, codigo, validade, parcelas, observacao);

        foreach (var item in itens)
        {
            orcamento.AdicionarItem(item);
        }
        
        return Result<Orcamento>.Success(orcamento);
    }
    
    public Result EditarDados(
        Guid clienteId,  
        Guid fornecedorId, 
        DateOnly validade, 
        int parcelas,
        string? observacao)
    {
        if(clienteId == Guid.Empty)
            return Result.Fail("O Cliente deve ser informado.");
        
        if(fornecedorId == Guid.Empty)
            return Result.Fail("O Fornecedor deve ser informado.");
        
        var vencimento = DateOnly.FromDateTime(DateTime.Now.AddDays(7));

        if (validade < vencimento)
            return Result<Orcamento>.Fail("A validade deve ser de uma semana ou mais");

        ClienteId = clienteId;
        FornecedorId = fornecedorId;
        Validade = validade;
        NumeroParcelas = parcelas;
        Observacao = observacao;
        
        return Result.Success();
    }
    
    public Result Enviar()
    {
        if (Status != StatusOrcamento.Rascunho)
            return Result.Fail("Apenas orçamentos em rascunho podem ser enviados.");
        
        var data = DateOnly.FromDateTime(DateTime.UtcNow);
        
        if(data > Validade)
            return Result.Fail("Não é possível enviar um orçamento vencido.");
        
        if(_itens.Count == 0)
            return Result.Fail("Não é possível enviar um orçamento sem itens.");
        
        Status = StatusOrcamento.Enviado;
        
        return Result.Success();
    }

    public Result Aprovar()
    {
        if(Status != StatusOrcamento.Enviado)
            return Result.Fail("Não é possivel aprovar um orçamento que não foi enviado.");
        
        var data = DateOnly.FromDateTime(DateTime.UtcNow);
        
        if(data > Validade)
            return Result.Fail("Não é possível aprovar um orçamento vencido.");

        if (Valor <= 0)
            return Result<Pedido>.Fail("Valor do orcamento inválido.");
            
        Status = StatusOrcamento.Aprovado;
        
        return Result.Success();
    }

    public Result Rejeitar()
    {
        if(Status != StatusOrcamento.Enviado)
            return Result.Fail("Não é possivel rejeitar um orçamento que não foi enviado.");
        
        Status = StatusOrcamento.Rejeitado;
        
        return Result.Success();
    }

    public Result Cancelar()
    {
        if(Status is StatusOrcamento.Rejeitado or StatusOrcamento.Aprovado)
            return Result.Fail("Não é possivel cancelar um orçamento com status: Aprovado ou Rejeitado.");
        
        Status = StatusOrcamento.Cancelado;
        
        return Result.Success();
    } 

    private void AdicionarItem(ItemOrcamento item)
    {
        _itens.Add(item);
    }
    
    public Result SubstituirItens(ICollection<ItemOrcamento> novosItens)
    {
        if (novosItens.Count == 0)
            return Result.Fail("O orçamento deve ter ao menos um item.");

        _itens.Clear();

        foreach (var item in novosItens)
            _itens.Add(item);

        return Result.Success();
    }
    
    public void Ativar() => IsActive = true;

    public Result Inativar()
    {
        if (Status != StatusOrcamento.Rascunho)
            return Result.Fail("É possivel deletar orcamento apenas com status: Rascunho");
        
        IsActive = false;
        
        return Result.Success();
    } 
}