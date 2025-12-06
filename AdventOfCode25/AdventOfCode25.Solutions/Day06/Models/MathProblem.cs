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

    protected long ApplyOperation(IEnumerable<long> values)
    {
        if (_operation is MathOperation.Addition) return values.Sum();

        if (_operation is MathOperation.Multiplication)
        {
            return values.Aggregate(1L, func: (a, b) => a * b);
        }

        return 0;
    }
}

public class HumanMathProblem : MathProblem
{
    public override long Calculate()
    {
        IEnumerable<long> parsedNumbers = _values.Select(long.Parse);
        return ApplyOperation(parsedNumbers);
    }
}

public class CephalopodMathProblem : MathProblem
{
    private Lazy<int> TotalNumberCount => new(() => _values.Select(x => x.Length).Max());

    public override long Calculate()
    {
        IEnumerable<long> parsedNumbers = Enumerable.Range(0, TotalNumberCount.Value)
            .Select(ParseNumberAtPosition);

        return ApplyOperation(parsedNumbers);
    }

    private long ParseNumberAtPosition(int position)
    {
        List<char> digits = [.. _values
            .Select(x => GetCharAtPosition(x, position))
            .OfType<char>()];

        return digits switch
        {
            [] when _operation is MathOperation.Addition => 0,
            [] when _operation is MathOperation.Multiplication => 1,
            _ => long.Parse(string.Join("", digits)),
        };
    }

    private static char? GetCharAtPosition(string toCheck, int position)
    {
        if (position >= toCheck.Length)
        {
            return null;
        }

        return toCheck[position] switch
        {
            ' ' => null,
            char notEmptyChar => notEmptyChar,
        };
    }
}

public class MathProblemCollection<T>
    where T : MathProblem, new()
{
    private readonly List<MathProblem> _mathProblemList;
    private readonly List<Range> _numberRanges;

    public long ProblemAnswerSum => _mathProblemList.Select(x => x.Calculate()).Sum();

    public MathProblemCollection(string operationInputLine)
    {
        List<int> operationIndices = 
        [
            ..operationInputLine.Index().Where(x => x.Item != ' ').Select(x => x.Index), 
            operationInputLine.Length
        ];

        _numberRanges = 
        [ 
            .. operationIndices[..^1]
                .Zip(operationIndices[1..])
                .Select(x => new Range(x.First, x.Second)),
        ];

        IEnumerable<T> mathProblems = Utils.SplitBySpaces<MathOperationWrapper>(operationInputLine)
            .Select(wrapper =>
            {
                T mathProblem = new();
                mathProblem.SetOperation(wrapper.Operation);
                return mathProblem;
            });

        _mathProblemList = [.. mathProblems];
    }

    public void HandleNumberLine(string inputLine)
    {
        foreach ((int index, Range range) in _numberRanges.Index())
        {
            string number = inputLine[range];
            _mathProblemList[index].AppendValue(number);
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