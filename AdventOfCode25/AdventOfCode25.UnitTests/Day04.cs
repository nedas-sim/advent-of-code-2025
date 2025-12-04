using AdventOfCode25.Solutions.Day04;

namespace AdventOfCode25.UnitTests;

public class Day04
{
    [Fact]
    public async Task TestSolution1()
    {
        int count = await Solution.CountForkliftAccessibleTilesAsync("sample", false);
        Assert.Equal(13, count);
    }

    [Fact]
    public async Task RunSolution1()
    {
        int count = await Solution.CountForkliftAccessibleTilesAsync("task", false);
        Assert.Equal(1370, count);
    }

    [Fact]
    public async Task TestSolution2()
    {
        int count = await Solution.CountForkliftAccessibleTilesAsync("sample", true);
        Assert.Equal(43, count);
    }

    [Fact]
    public async Task RunSolution2()
    {
        int count = await Solution.CountForkliftAccessibleTilesAsync("task", true);
        Assert.Equal(8437, count);
    }
}
