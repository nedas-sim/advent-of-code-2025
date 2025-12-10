using AdventOfCode25.Solutions.Day09;

namespace AdventOfCode25.UnitTests;

public class Day09
{
    [Fact]
    public async Task TestSolution1()
    {
        long result = await Solution.GetLargestSquareAreaAsync("sample");
        Assert.Equal(50, result);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long result = await Solution.GetLargestSquareAreaAsync("task");
        Assert.Equal(4738108384, result);
    }

    [Fact]
    public async Task TestSolution2()
    {
        long result = await Solution.GetLargestContainedRectangleAreaAsync("sample", true);
        Assert.Equal(24, result);
    }

    [Fact]
    public async Task RunSolution2()
    {
        long result = await Solution.GetLargestContainedRectangleAreaAsync("task", true);

        Assert.NotEqual(86716069, result);
        Assert.NotEqual(182366626, result);

        Assert.Equal(1, result);
    }
}
