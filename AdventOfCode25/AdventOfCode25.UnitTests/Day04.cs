using AdventOfCode25.Solutions.Day04;

namespace AdventOfCode25.UnitTests;

public class Day04
{
    [Fact]
    public async Task TestSolution1()
    {
        int count = await Solution.CountForkliftAccessibleTilesAsync("sample");
        Assert.Equal(13, count);
    }

    [Fact]
    public async Task RunSolution1()
    {
        int count = await Solution.CountForkliftAccessibleTilesAsync("task");
        Assert.Equal(1370, count);
    }

    /*[Fact]
    public async Task TestSolution2()
    {
        long sum = await Solution.SumJoltagesAsync("sample", 12);
        Assert.Equal(3121910778619, sum);
    }

    [Fact]
    public async Task RunSolution2()
    {
        long sum = await Solution.SumJoltagesAsync("task", 12);
        Assert.Equal(168575096286051, sum);
    }*/
}
