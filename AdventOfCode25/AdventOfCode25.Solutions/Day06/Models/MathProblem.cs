using AdventOfCode25.Solutions.Shared;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode25.Solutions.Day06.Models;

public class MathProblem(long firstValue)
{
    private MathOperation _operation;
    private readonly List<long> _values = [firstValue];

    public void AppendValue(long value) => _values.Add(value);

    public void SetOperation(MathOperation op) => _operation = op;

    public long Calculate()
    {
        if (_operation == MathOperation.Addition) return _values.Sum();
        if (_operation == MathOperation.Multiplication) return _values.Aggregate(1L, func: (a, b) => a * b);

        return 0;
    }
}

public class MathProblemCollection
{
    private List<MathProblem> _list = [];

    public long ProblemAnswerSum => _list.Select(x => x.Calculate()).Sum();

    public void HandleFirstLine(string inputLine)
    {
        _list = [ ..Utils.SplitBySpaces<long>(inputLine).Select(x => new MathProblem(x)) ];
    }

    public void HandleOtherLine(string inputLine)
    {
        try
        {
            foreach ((int problemIndex, MathOperationWrapper wrapper) in Utils.SplitBySpaces<MathOperationWrapper>(inputLine).Index())
            {
                _list[problemIndex].SetOperation(wrapper.Operation);
            }
        }
        catch (InvalidOperationException)
        {
            foreach ((int problemIndex, long number) in Utils.SplitBySpaces<long>(inputLine).Index())
            {
                _list[problemIndex].AppendValue(number);
            }
        }
    }
}

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