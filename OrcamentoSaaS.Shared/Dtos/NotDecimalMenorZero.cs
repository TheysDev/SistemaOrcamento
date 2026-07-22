using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos;

public class NotDecimalMenorZero : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
            return true;
        
        return value is decimal and > 0;
    }
}