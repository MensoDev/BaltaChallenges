namespace BaltaChallenges;

public static class CardinalConverter
{
    private const long QuadrillionUnit = 1_000_000_000_000_000;
    private const long QuadrillionMax = 999_999_999_999_999_999;

    private const long TrillionUnit = 1_000_000_000_000;
    private const long TrillionMax = 999_999_999_999_999;

    private const long BillionUnit = 1_000_000_000;
    private const long BillionMax = 999_999_999_999;

    private const long MillionUnit = 1_000_000;
    private const long MillionMax = 999_999_999;

    private const long ThousandUnit = 1_000;
    private const long ThousandMax = 999_999;

    private const long HundredUnit = 100;
    private const long HundredMax = 999;

    private const long TenUnit = 10;
    private const long TenMax = 99;

    private const char ConjunctionToken = 'e';

    private static readonly long[] NumbersPrefix = Enumerable.Range(1, 999).Select(n => (long)n).ToArray();


    public static string ToCardinalName(long number)
    {
        return number switch
        {
            0 => string.Empty,
            <= 9 => ProcessUnit(number),
            <= 19 => ProcessFirstTen(number),
            <= TenMax => ProcessTen(number),
            <= HundredMax => ProcessHundred(number),
            <= ThousandMax => ProcessBigNumbers(number, ThousandUnit),
            <= MillionMax => ProcessBigNumbers(number, MillionUnit),
            <= BillionMax => ProcessBigNumbers(number, BillionUnit),
            <= TrillionMax => ProcessBigNumbers(number, TrillionUnit),
            <= QuadrillionMax => ProcessBigNumbers(number, QuadrillionUnit),
            _ => string.Empty
        };
    }

    private static string ProcessUnit(long number)
    {
        return number switch
        {
            1 => "um",
            2 => "dois",
            3 => "três",
            4 => "quatro",
            5 => "cinco",
            6 => "seis",
            7 => "sete",
            8 => "oito",
            9 => "nove",
            _ => throw new ArgumentOutOfRangeException($"{number} is not unit number")
        };
    }

    private static string ProcessFirstTen(long number)
    {
        return number switch
        {
            10 => "dez",
            11 => "onze",
            12 => "doze",
            13 => "treze",
            14 => "quatorze",
            15 => "quinze",
            16 => "dezesseis",
            17 => "dezessete",
            18 => "dezoito",
            19 => "dezenove",
            _ => throw new ArgumentOutOfRangeException($"{number} is not first ten number")
        };
    }

    private static string ProcessTen(long number)
    {
        var firstDigit = GetFirsNumber(number);

        number -= firstDigit * TenUnit;

        return firstDigit switch
        {
            2 => MakeWithSufixeCaseExiste("vinte", ConjunctionToken, ToCardinalName(number)),
            3 => MakeWithSufixeCaseExiste("trinta", ConjunctionToken, ToCardinalName(number)),
            4 => MakeWithSufixeCaseExiste("quarenta", ConjunctionToken, ToCardinalName(number)),
            5 => MakeWithSufixeCaseExiste("cinquenta", ConjunctionToken, ToCardinalName(number)),
            6 => MakeWithSufixeCaseExiste("sessenta", ConjunctionToken, ToCardinalName(number)),
            7 => MakeWithSufixeCaseExiste("setenta", ConjunctionToken, ToCardinalName(number)),
            8 => MakeWithSufixeCaseExiste("oitenta", ConjunctionToken, ToCardinalName(number)),
            9 => MakeWithSufixeCaseExiste("noventa", ConjunctionToken, ToCardinalName(number)),
            _ => throw new ArgumentOutOfRangeException($"{number} is not ten number")
        };
    }

    private static string ProcessHundred(long number)
    {
        var firstDigit = GetFirsNumber(number);

        number -= firstDigit * HundredUnit;

        return firstDigit switch
        {
            1 => MakeWithSufixeCaseExiste(number <= 0 ? "cem" : "cento", ConjunctionToken, ToCardinalName(number)),
            2 => MakeWithSufixeCaseExiste("duzentos", ConjunctionToken, ToCardinalName(number)),
            3 => MakeWithSufixeCaseExiste("trezentos", ConjunctionToken, ToCardinalName(number)),
            4 => MakeWithSufixeCaseExiste("quatrocentos", ConjunctionToken, ToCardinalName(number)),
            5 => MakeWithSufixeCaseExiste("quinhentos", ConjunctionToken, ToCardinalName(number)),
            6 => MakeWithSufixeCaseExiste("seiscentos", ConjunctionToken, ToCardinalName(number)),
            7 => MakeWithSufixeCaseExiste("setecentos", ConjunctionToken, ToCardinalName(number)),
            8 => MakeWithSufixeCaseExiste("oitocentos", ConjunctionToken, ToCardinalName(number)),
            9 => MakeWithSufixeCaseExiste("novecentos", ConjunctionToken, ToCardinalName(number)),
            _ => throw new ArgumentOutOfRangeException($"{number} is not ten number")
        };
    }

    private static string ProcessBigNumbers(long number, long unit)
    {
        var index = NumbersPrefix.FindIndexByBinarySearch(number, unit);
        var units = NumbersPrefix[index];
        number -= units * unit;
        return MakeWithSufixeCaseExiste($"{ToCardinalName(units)} {TranslateUnitToString(unit, units)}",
            ToCardinalName(number));
    }

    private static string TranslateUnitToString(long unit, long units)
    {
        return unit switch
        {
            ThousandUnit => "mil",
            MillionUnit => units > 1 ? "milhões" : "milhão",
            BillionUnit => units > 1 ? "bilhões" : "bilhão",
            TrillionUnit => units > 1 ? "trilhões" : "trilhão",
            QuadrillionUnit => units > 1 ? "quatrilhões" : "quatrilhão",
            _ => throw new ArgumentOutOfRangeException($"unit {unit} not mapped")
        };
    }


    private static string MakeWithSufixeCaseExiste(string prefix, char separator, string sufixe)
        => string.IsNullOrEmpty(sufixe) ? prefix : $"{prefix} {separator} {sufixe}";

    private static string MakeWithSufixeCaseExiste(string prefix, string sufixe)
        => string.IsNullOrEmpty(sufixe) ? prefix : $"{prefix} {sufixe}";

    private static long GetFirsNumber(long number) => (long)number.ToString()[0] - 48;
}