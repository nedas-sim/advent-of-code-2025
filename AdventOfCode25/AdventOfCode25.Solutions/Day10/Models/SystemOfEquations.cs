using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode25.Solutions.Day10.Models;

public class Equation
{
    public required List<decimal> Coefficients { get; set; }

    public string CoefficientsUpToLast() => string.Join("", Coefficients[..^1]);

    public EquationResult ApplyAndCompare(int[] variables)
    {
        decimal sum = Apply(variables);

        int compareValue = sum.CompareTo(Coefficients[^1]);

        if (compareValue == 0) return EquationResult.Equal;
        if (compareValue > 0) return EquationResult.More;
        return EquationResult.JustContinue;
    }

    public (int VariableIndex, decimal VariableValue)? GetSingleVariableSolution()
    {
        List<(int Index, decimal Item)> nonZeroCoefficients = Coefficients[..^1]
            .Index()
            .Where(x => x.Item != 0)
            .Take(2)
            .ToList();

        if (nonZeroCoefficients is not [(int Index, decimal Item) nonZeroCoefficient])
        {
            return null;
        }

        return (nonZeroCoefficient.Index, Coefficients[^1] / nonZeroCoefficient.Item);
    }

    public bool ApplyVariableSolution(int index, decimal solution)
    {
        const decimal epsilon = 0.0001M;
        Coefficients[^1] -= Coefficients[index] * solution;
        Coefficients = Coefficients.Index().Where(x => x.Index != index).Select(x => x.Item).ToList();
        return Coefficients.All(x => -epsilon < x && x < epsilon);
    }

    private decimal Apply(int[] variables)
    {
        return variables.Zip(Coefficients).Select(x => x.First * x.Second).Sum();
    }
}

public enum EquationResult
{
    Equal = 1,
    More,
    JustContinue,
}

public class SystemOfEquations
{
    private readonly List<WiringSchematic> _schematics = [];
    private readonly List<decimal> _joltageRequirements = [];

    private List<Equation> _equations = [];

    private Equation this[int row]
    {
        get => _equations[row];
    }

    private decimal this[int row, int col]
    {
        get => _equations[row].Coefficients[col];
        set => _equations[row].Coefficients[col] = value;
    }

    public int Solution { get; private set; } = 0;

    public int Solve(int index)
    {
        if (_equations is [])
        {
            Console.WriteLine($"No equations left for '{index}'. Solution is: {Solution}");
            return Solution;
        }

        long startTimestamp = Stopwatch.GetTimestamp();
        Console.WriteLine($"Starting problem solving for '{index}' at {DateTimeOffset.Now}");

        List<EquatableArray> forCurrentLevel = GetCombinationProduct().ToList();

        EquatableArrayComparer comparer = new();

        while (true)
        {
            HashSet<EquatableArray> forNextLevel = new(comparer);

            foreach (EquatableArray combination in forCurrentLevel)
            {
                Dictionary<EquationResult, bool> equationStatusToBool = _equations
                    .Select(eq => eq.ApplyAndCompare(combination.Items))
                    .GroupBy(x => x)
                    .ToDictionary(x => x.Key, x => x.Any());

                bool safeAccess(EquationResult res)
                {
                    if (equationStatusToBool.TryGetValue(res, out bool value))
                    {
                        return value;
                    }

                    return false;
                }

                if (!safeAccess(EquationResult.More) && !safeAccess(EquationResult.JustContinue))
                {
                    TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTimestamp);
                    Console.WriteLine($"Finishing problem solving for '{index}' at {DateTimeOffset.Now}, it ran for {elapsedTime}. Solution is: {Solution}");

                    return Solution;
                }

                if (!safeAccess(EquationResult.JustContinue))
                {
                    continue;
                }

                foreach (EquatableArray nextToAdd in MultiplyCombinations(combination.Items))
                {
                    forNextLevel.Add(nextToAdd);
                }
            }
            
            forCurrentLevel = [.. forNextLevel];
            forNextLevel = new(comparer);
            Solution++;
        }
    }

    private IEnumerable<EquatableArray> MultiplyCombinations(int[] combinations)
    {
        foreach (EquatableArray increments in GetCombinationProduct())
        {
            yield return new EquatableArray
            {
                Items = combinations.Zip(increments.Items)
                    .Select(x => x.First + x.Second)
                    .ToArray(),
            };
        }
    }

    private IEnumerable<EquatableArray> GetCombinationProduct()
    {
        for (int i = 0; i < this[0].Coefficients.Count - 1; i++)
        {
            int[] toYield = new int[this[0].Coefficients.Count - 1];
            toYield[i] = 1;

            yield return new EquatableArray
            {
                Items = toYield,
            };
        }
    }

    public SystemOfEquations Simplify()
    {
        while (true)
        {
            (int VariableIndex, decimal VariableValue)? variableSolution = _equations
                .Select(eq => eq.GetSingleVariableSolution())
                .Where(x => x.HasValue)
                .FirstOrDefault();

            if (variableSolution is null)
            {
                break;
            }

            (int index, decimal value) = variableSolution.Value;

            decimal positiveValue = Math.Abs(value);

            Solution += (int)positiveValue;

            _equations = _equations
                .Where(x => !x.ApplyVariableSolution(index, value))
                .ToList();
        }

        return this;
    }

    public SystemOfEquations DoGaussianElimination(int index)
    {
        long startTimestamp = Stopwatch.GetTimestamp();
        Console.WriteLine($"Starting Gaussian elimination for '{index}' at {DateTimeOffset.Now}");

        int h = 0, k = 0;
        int m = _equations.Count;
        int n = this[0].Coefficients.Count;
        
        while (h < m && k < n)
        {
            int i_max = Enumerable
                .Range(h, m - h)
                .Select(i => (Index: i, Value: Math.Abs(this[i, k])))
                .MaxBy(x => x.Value)
                .Index;

            if (this[i_max, k] == 0)
            {
                k++;
                continue;
            }

            (_equations[h], _equations[i_max]) = (_equations[i_max], _equations[h]);
            for (int i = h + 1; i < m; i++)
            {
                decimal f = this[i, k] / this[h, k];
                this[i, k] = 0;
                for (int j = k + 1; j < n; j++)
                {
                    this[i, j] = this[i, j] - this[h, j] * f;
                }
            }
            h++;
            k++;
        }

        TimeSpan elapsedTime = Stopwatch.GetElapsedTime(startTimestamp);
        Console.WriteLine($"Finishing Gaussian elimination for '{index}' at {DateTimeOffset.Now}, it ran for {elapsedTime}");

        return this;
    }

    public static SystemOfEquations Create(string inputLine)
    {
        SystemOfEquations soe = new();

        ReadOnlySpan<char> remainingString = inputLine.AsSpan()[inputLine.IndexOf(']')..];

        while (true)
        {
            int leftParenIndex = remainingString.IndexOf('(');
            int rightParenIndex = remainingString.IndexOf(')');

            if (leftParenIndex < 0 || rightParenIndex < 0)
            {
                break;
            }

            ReadOnlySpan<char> toggleIndices = remainingString[(leftParenIndex + 1)..rightParenIndex];
            WiringSchematic schematic = new();

            foreach (Range singleIndexRange in toggleIndices.Split(','))
            {
                schematic.ToggleIndices.Add(int.Parse(toggleIndices[singleIndexRange]));
            }

            soe._schematics.Add(schematic);

            remainingString = remainingString[(rightParenIndex + 1)..];
        }

        int from = remainingString.IndexOf('{') + 1;
        int to = remainingString.IndexOf('}');

        soe._joltageRequirements
            .AddRange(remainingString[from..to]
                .ToString()
                .Split(',')
                .Select(decimal.Parse));

        soe.BuildEquations();

        return soe;
    }

    private void BuildEquations()
    {
        for (int joltageIndex = 0; joltageIndex < _joltageRequirements.Count; joltageIndex++)
        {
            List<int> indicesToHaveValueOne = _schematics
                .Index()
                .Where(s => s.Item.ToggleIndices.Contains(joltageIndex))
                .Select(s => s.Index)
                .ToList();

            decimal[] coeffs = new decimal[_schematics.Count + 1];

            indicesToHaveValueOne.ForEach(x => coeffs[x] = 1);
            coeffs[^1] = _joltageRequirements[joltageIndex];

            _equations.Add(new Equation
            {
                Coefficients = [.. coeffs],
            });
        }

        _equations = _equations
            .OrderByDescending(x => x.CoefficientsUpToLast())
            .ToList();
    }
}

public class EquatableArrayComparer : IEqualityComparer<EquatableArray>
{
    public bool Equals(EquatableArray x, EquatableArray y)
    {
        return x.Equals(y);
    }

    public int GetHashCode([DisallowNull] EquatableArray obj)
    {
        return obj.GetHashCode();
    }
}

public readonly struct EquatableArray : IEquatable<EquatableArray>
{
    public required int[] Items { get; init; }

    public readonly bool Equals(EquatableArray other)
    {
        return other.Items.SequenceEqual(Items);
    }

    public override readonly int GetHashCode()
    {
        HashCode hc = new();

        foreach (int x in Items) hc.Add(x);

        return hc.ToHashCode();
    }

    public override readonly bool Equals(object obj)
    {
        return obj is EquatableArray array && Equals(array);
    }
}
