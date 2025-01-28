using System.Collections.Generic;
using IdentityService.Domain.Base;

namespace IdentityService.Domain.ValueObjects;

public class Cpf : ValueObject
{
    public Cpf(string value)
    {
        var cpf = Sanitize(value);

        Value = cpf;
    }

    public string Value { get; }

    private static string Sanitize(string value)
    {
        var cpf = value.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        return cpf;
    }

    // source: https://www.macoratti.net/11/09/c_val1.htm
    public bool IsValid()
    {
        var cpf = Value;
        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (var i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (var i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto;
        return cpf.EndsWith(digito);
    }

    public static bool TryParse(string value, out Cpf cpf)
    {
        cpf = new Cpf(value);

        return cpf.IsValid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}