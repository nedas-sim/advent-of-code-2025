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
        /*int index = 31;
        SystemOfEquations forTest = SystemOfEquations.Create(File.ReadLines($"./Day10/{fileName}.txt").ToList()[index]);
        forTest.DoGaussianElimination(index);
        forTest.Simplify();
        return forTest.Solve(index);*/

        List<SystemOfEquations> SOEs = File.ReadLines($"./Day10/{fileName}.txt")
            .Select(SystemOfEquations.Create)
            .Index().Select(x => x.Item.DoGaussianElimination(x.Index))
            .Index().Select(x => x.Item.Simplify())
            .ToList();

        Parallel.ForEach(SOEs.Index(), x => x.Item.Solve(x.Index));

        return SOEs.Select(x => x.Solution).Sum() + SOEs.Count;
    }
}
