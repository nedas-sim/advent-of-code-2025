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
        return _operation switch
        {
            MathOperation.Addition => values.Sum(),
            MathOperation.Multiplication => values.Aggregate(1L, func: (a, b) => a * b),
            _ => throw new InvalidOperationException($"{_operation}"),
        };
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
        IEnumerable<long> parsedNumbers = Enumerable
            .Range(0, TotalNumberCount.Value)
            .Select(ParseNumberAtPosition);

        return ApplyOperation(parsedNumbers);
    }

    private long ParseNumberAtPosition(int position)
    {
        List<char> digits = [.. _values
            .Select(x => GetCharAtPosition(x, position))
            .OfType<char>()];

        return (digits, _operation) switch
        {
            ([], MathOperation.Addition) => 0,
            ([], MathOperation.Multiplication) => 1,
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
