using OrcamentoSaaS.Shared.Results;

namespace OrcamentoSaaS.Shared.ValueObjects;

public class ProdutoDetalhes
{
    public decimal? Comprimento {get; private set;}
    public decimal? Peso {get; private set;}
    public string? Diametro {get; private set;}
    public decimal? Volume {get; private set;}

    private ProdutoDetalhes(decimal? comprimento, decimal? peso, decimal? volume, string? diametro)
    {
        Comprimento = comprimento;
        Peso = peso;
        Volume = volume;
        Diametro = diametro;
    }

    public static Result<ProdutoDetalhes> Criar(decimal? comprimento, decimal? peso, 
        decimal? volume, string? diametro)
    {
        if (comprimento < 0)
            return Result<ProdutoDetalhes>.Fail("O Comprimento não pode ser nagativo");
        
        if (peso < 0)
            return Result<ProdutoDetalhes>.Fail("O Peso não pode ser nagativo");
        
        if (volume < 0)
            return Result<ProdutoDetalhes>.Fail("O Volume não pode ser nagativo");

        return Result<ProdutoDetalhes>.Success(new
            ProdutoDetalhes(comprimento, peso, volume, diametro));
    }
}

    