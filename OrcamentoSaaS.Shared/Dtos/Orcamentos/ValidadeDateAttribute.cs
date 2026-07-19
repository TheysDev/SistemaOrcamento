using System.ComponentModel.DataAnnotations;

namespace OrcamentoSaaS.Shared.Dtos.Orcamentos;

public class ValidadeDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateOnly validade) 
            return false;
        
        var vencimento = DateOnly.FromDateTime(DateTime.Now.AddDays(7));
        
        return validade >= vencimento;
    }
}