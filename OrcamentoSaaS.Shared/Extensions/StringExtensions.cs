namespace OrcamentoSaaS.Shared.Extensions;

public static class StringExtensions
{
    public static string ApenasNumeros(this string valor)
    {
        return new string(valor
            ?.Where(char.IsDigit)
            .ToArray() ?? []);
    }
}