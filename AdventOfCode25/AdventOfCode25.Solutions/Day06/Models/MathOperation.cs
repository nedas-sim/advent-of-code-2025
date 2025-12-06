using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode25.Solutions.Day06.Models;

public record struct MathOperationWrapper(MathOperation Operation) : IParsable<MathOperationWrapper>
{
    public static MathOperationWrapper Parse(string s, IFormatProvider? provider)
    {
        return s switch
        {
            "+" => new MathOperationWrapper(MathOperation.Addition),
            "*" => new MathOperationWrapper(MathOperation.Multiplication),
            _ => throw new InvalidOperationException(s),
        };
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out MathOperationWrapper result)
    {
        throw new NotImplementedException();
    }
}

public enum MathOperation
{
    Addition = 1,
    Multiplication,
}

public class InvalidOperationException(string s) : Exception($"Kaip sakot? Operation '{s}'???");