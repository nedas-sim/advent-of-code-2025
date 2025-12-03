using AdventOfCode25.Solutions.Day03;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode25.Benchmarks;

[MemoryDiagnoser]
public class Day03Benchmarks
{
    [Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
    public int DigitsLength;

    [Benchmark]
    public async Task FirstTaskAsync()
    {
        await Solution.SumJoltagesAsync("task", DigitsLength);
    }
}
