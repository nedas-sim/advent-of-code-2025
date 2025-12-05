using AdventOfCode25.Solutions.Day05;
using AdventOfCode25.Solutions.Day05.Models;

namespace AdventOfCode25.UnitTests;

public class Day05
{
    [Fact]
    public async Task TestSolution1()
    {
        long freshIdCount = await Solution.CountFreshIdsAsync<RangeOfIndividualSamples>("sample");
        Assert.Equal(3, freshIdCount);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long freshIdCount = await Solution.CountFreshIdsAsync<RangeOfIndividualSamples>("task");
        Assert.Equal(505, freshIdCount);
    }

    [Fact]
    public async Task TestSolution2()
    {
        long freshIdCount = await Solution.CountFreshIdsAsync<RangeOfMinMaxValues>("sample");
        Assert.Equal(14, freshIdCount);
    }

    [Fact]
    public async Task RunSolution2()
    {
        long freshIdCount = await Solution.CountFreshIdsAsync<RangeOfMinMaxValues>("task");
        Assert.Equal(344423158480189, freshIdCount);
    }
}
