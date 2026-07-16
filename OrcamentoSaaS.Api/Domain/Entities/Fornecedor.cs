namespace OrcamentoSaaS.Api.Domain.Entities;

public class Fornecedor
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Cidade { get; private set; } = null!;
    public string Uf { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? Telefone { get; private set; } = null!;
    public Documento Documento { get; private set; } = null!;
    public decimal PorcentagemAVista { get; private set; }
    public decimal PorcentagemAPrazo { get; private set; }
    public bool IsActive { get; private set; }

    protected Fornecedor()
    {}

    private Fornecedor(Guid tenantId, string nome, string cidade, string uf, string email, string? telefone, Documento documento,
        decimal porcentagemAVista, decimal porcentagemAPrazo)
    {
        TenantId = tenantId;
        Nome = nome;
        Cidade = cidade;
        Uf = uf;
        Email = email;
        Telefone = telefone;
        Documento = documento;
        PorcentagemAVista = porcentagemAVista;
        PorcentagemAPrazo = porcentagemAPrazo;
        IsActive = true;
    }

    public static Result<Fornecedor> Criar(
        Guid tenantId, string nome, string cidade, string uf, string email, string? telefone, Documento documento,
        decimal porcentagemAVista, decimal porcentagemAPrazo)
    {
        if (porcentagemAVista < 0)
            return Result<Fornecedor>.Fail("A Porcentagem a vista não pode ser negativa");
        
        if (porcentagemAPrazo < 0)
            return Result<Fornecedor>.Fail("A Porcentagem a prazo não pode ser negativa");

        return Result<Fornecedor>.Success(
            new Fornecedor(
                tenantId, 
                nome, 
                cidade, 
                uf, 
                email, 
                telefone, 
                documento, 
                porcentagemAVista, 
                porcentagemAPrazo));
    }
    
    public void EditarDados(string nome, string cidade, string uf, string email, string telefone, Documento documento)
    {
        Nome = nome;
        Cidade = cidade;
        Uf = uf;
        Email = email;
        Telefone = telefone;
        Documento = documento;
    }
    
    public void Ativar() => IsActive = true;
    
    public void Inativar() => IsActive = false;

    public Result EditarPorcentagemAvista(decimal porcentagem)
    {
        if (porcentagem < 0)
            return Result.Fail("Porcentagem não pode ser negativa");

        PorcentagemAVista = porcentagem;

        return Result.Success();
    }
    
    public Result EditarPorcentagemAprazo(decimal porcentagem)
    {
        if (porcentagem < 0)
            return Result.Fail("Porcentagem não pode ser negativa");
        
        PorcentagemAPrazo = porcentagem;
        
        return Result.Success();
    }
}