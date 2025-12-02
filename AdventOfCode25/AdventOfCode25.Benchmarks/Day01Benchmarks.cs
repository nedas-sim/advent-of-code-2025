using AdventOfCode25.Solutions.Day01;
using AdventOfCode25.Solutions.Day01.Models;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode25.Benchmarks;

[MemoryDiagnoser]
public class Day01Benchmarks
{
    [Benchmark]
    public async Task FirstTaskAsync()
    {
        await Solution.CalculateTotalZerosOnTurnEndAsync("task");
    }

    [Benchmark]
    public async Task SecondTaskAsync()
    {
        await Solution.CalculateTotalZerosAtAnyPointAsync("task");
    }
}
