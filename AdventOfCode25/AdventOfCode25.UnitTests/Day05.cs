using AdventOfCode25.Solutions.Day05;

namespace AdventOfCode25.UnitTests;

public class Day05
{
    [Fact]
    public async Task TestSolution1()
    {
        int freshIdCount = await Solution.CountFreshIdsAsync("sample");
        Assert.Equal(3, freshIdCount);
    }

    [Fact]
    public async Task RunSolution1()
    {
        int freshIdCount = await Solution.CountFreshIdsAsync("task");
        Assert.Equal(505, freshIdCount);
    }
}
