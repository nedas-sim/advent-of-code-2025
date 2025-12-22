using AdventOfCode25.Solutions.Day10.Models;

namespace AdventOfCode25.Solutions.Day10;

public class Solution
{
    public static async Task<long> SumMinimalToggleCountsAsync(string fileName)
    {
        return File.ReadLines($"./Day10/{fileName}.txt")
            .Select(x => new Machine(x))
            .Select(x => x.Solve())
            .Sum();
    }

    public static async Task<long> DesimtDuAsync(string fileName)
    {
        return File.ReadLines($"./Day10/{fileName}.txt")
            .Select(SystemOfEquations.Create)
            .Select(x => x.DoGaussianElimination())
            .Select(x => x.Solve())
            .Sum();
    }
}
