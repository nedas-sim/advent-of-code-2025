using AdventOfCode25.Solutions.Day07;

namespace AdventOfCode25.UnitTests;

public class Day07
{
    [Fact]
    public async Task TestSolution1()
    {
        long splits = await Solution.CountSplits("sample", false);
        Assert.Equal(21, splits);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long splits = await Solution.CountSplits("task", false);
        Assert.Equal(1619, splits);
    }

    [Fact]
    public async Task TestSolution2()
    {
        long splits = await Solution.CountSplits("sample", true);
        Assert.Equal(40, splits);
    }

    [Fact]
    public async Task RunSolution2()
    {
        long splits = await Solution.CountSplits("task", true);
        Assert.Equal(23607984027985, splits);
    }
}
