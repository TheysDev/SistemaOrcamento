using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos;

public class NotDescontoMenorZero : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is decimal and >= 0;
    }
}