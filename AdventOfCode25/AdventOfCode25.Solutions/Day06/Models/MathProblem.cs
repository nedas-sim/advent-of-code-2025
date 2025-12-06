using AdventOfCode25.Solutions.Shared;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode25.Solutions.Day06.Models;

public abstract class MathProblem
{
    protected MathOperation _operation;
    protected readonly List<string> _values = [];

    public void AppendValue(string value) => _values.Add(value);

    public void SetOperation(MathOperation op) => _operation = op;

    public abstract long Calculate();
}

public class HumanMathProblem : MathProblem
{
    public override long Calculate()
    {
        IEnumerable<long> parsedNumbers = _values.Select(long.Parse);

        if (_operation is MathOperation.Addition) return parsedNumbers.Sum();
        
        if (_operation is MathOperation.Multiplication)
        {
            return parsedNumbers.Aggregate(1L, func: (a, b) => a * b);
        }

        return 0;
    }
}

public class MathProblemCollection<T>
    where T : MathProblem, new()
{
    private List<MathProblem> _list = [];

    public long ProblemAnswerSum => _list.Select(x => x.Calculate()).Sum();

    public void HandleFirstLine(string inputLine)
    {
        IEnumerable<T> mathProblems = Utils.SplitBySpaces<string>(inputLine)
            .Select(x =>
            {
                T mathProblem = new();
                mathProblem.AppendValue(x);
                return mathProblem;
            });

        _list = [ .. mathProblems ];
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
            foreach ((int problemIndex, string value) in Utils.SplitBySpaces<string>(inputLine).Index())
            {
                _list[problemIndex].AppendValue(value);
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