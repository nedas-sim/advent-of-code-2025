using AdventOfCode25.Solutions.Day10;

namespace AdventOfCode25.UnitTests;

public class Day10
{
    [Fact]
    public async Task TestSolution1()
    {
        long result = await Solution.SumMinimalToggleCountsAsync("sample");
        Assert.Equal(7, result);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long result = await Solution.SumMinimalToggleCountsAsync("task");
        Assert.Equal(532, result);
    }

    [Fact]
    public async Task TestSolution2()
    {
        long result = await Solution.DesimtDuAsync("sample");
        Assert.Equal(33, result);
    }
}
