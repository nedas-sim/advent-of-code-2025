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
        long result = await Solution.GetLargestContainedSquareAreaAsync("sample");
        Assert.Equal(24, result);
    }

    [Fact]
    public async Task RunSolution2()
    {
        long result = await Solution.GetLargestContainedSquareAreaAsync("task");

        Assert.NotEqual(43402179, result);
        Assert.NotEqual(4582310446, result);
        Assert.NotEqual(4576741172, result);

        Assert.True(result < 4738108384); // Solution1 answer

        Assert.Equal(1, result); // Show result in error message
    }
}
