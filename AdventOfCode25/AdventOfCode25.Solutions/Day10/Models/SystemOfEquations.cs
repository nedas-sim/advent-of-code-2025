using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode25.Solutions.Day10.Models;

public class Equation
{
    public required List<decimal> Coefficients { get; set; }

    public string CoeeficientsUpToLast() => string.Join("", Coefficients[..^1]);

    public EquationResult ApplyAndCompare(int[] variables)
    {
        decimal sum = Apply(variables);

        int compareValue = sum.CompareTo(Coefficients[^1]);

        if (compareValue == 0) return EquationResult.Equal;
        if (compareValue > 0) return EquationResult.More;
        return EquationResult.JustContinue;
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

    public int Solve()
    {
        int depth = 1;

        List<EquatableArray> forCurrentLevel = GetCombinationProduct().ToList();

        EquatableArrayComparer comparer = new();

        while (true)
        {
            HashSet<EquatableArray> forNextLevel = new(comparer);

            foreach (EquatableArray combination in forCurrentLevel)
            {
                Dictionary<EquationResult, int> equationStatusToBool = _equations
                    .Select(eq => eq.ApplyAndCompare(combination.Items))
                    .GroupBy(x => x)
                    .ToDictionary(x => x.Key, x => x.Count());

                bool safeAccess(EquationResult res)
                {
                    if (equationStatusToBool.TryGetValue(res, out int value))
                    {
                        return value > 0;
                    }

                    return false;
                }

                if (!safeAccess(EquationResult.More) && !safeAccess(EquationResult.JustContinue))
                {
                    return depth;
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
            depth++;
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

    public SystemOfEquations DoGaussianElimination()
    {
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
            .OrderByDescending(x => x.CoeeficientsUpToLast())
            .ToList();
    }
}

public class EquatableArrayComparer : IEqualityComparer<EquatableArray>
{
    public bool Equals(EquatableArray? x, EquatableArray? y)
    {
        return x?.Equals(y) ?? false;
    }

    public int GetHashCode([DisallowNull] EquatableArray obj)
    {
        return obj.GetHashCode();
    }
}

public class EquatableArray : IEquatable<EquatableArray>
{
    public required int[] Items { get; init; }

    public bool Equals(EquatableArray? other)
    {
        if (other is null)
        {
            return this is null;
        }

        return other.Items.SequenceEqual(Items);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as EquatableArray);
    }

    public override int GetHashCode()
    {
        HashCode hc = new();

        foreach (int x in Items) hc.Add(x);

        return hc.ToHashCode();
    }
}
