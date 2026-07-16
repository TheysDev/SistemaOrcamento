using CpfCnpjLibrary;
using OrcamentoSaaS.Shared.Extensions;
using OrcamentoSaaS.Shared.Result;

namespace OrcamentoSaaS.Shared.ValueObjects;

public sealed record Documento
{
    public string Valor { get; } = null!;
    
    private Documento(string valor)
    {
        Valor = valor;
    }

    public static Result<Documento> Criar(string valor)
    {
        var documento = valor.ApenasNumeros();
        
        if (string.IsNullOrWhiteSpace(documento))
            return Result<Documento>.Fail("Documento obrigatório");

        if (Cpf.Validar(documento) || Cnpj.Validar(documento))
            return Result<Documento>.Success(
                new Documento(documento));

        return Result<Documento>.Fail("CPF/CNPJ inválido");
    }
    
    public static implicit operator string(Documento documento) => documento.Valor;
    
    public override string ToString()
    {
        return Valor;
    }
    
    public static Documento FromPersistence(string valor)
    {
        return new Documento(valor);
    }
}
