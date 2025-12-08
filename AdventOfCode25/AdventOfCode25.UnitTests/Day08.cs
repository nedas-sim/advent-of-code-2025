using AdventOfCode25.Solutions.Day08;

namespace AdventOfCode25.UnitTests;

public class Day08
{
    [Fact]
    public async Task TestSolution1()
    {
        long result = await Solution.CalculateDistanceProductAsync("sample", 10);
        Assert.Equal(40, result);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long result = await Solution.CalculateDistanceProductAsync("task", 1000);
        Assert.Equal(54600, result);
    }

    [Fact]
    public async Task TestSolution2()
    {
        long result = await Solution.CalculateLastXCoordinateProduct("sample");
        Assert.Equal(25272, result);
    }

    [Fact]
    public async Task RunSolution2()
    {
        long result = await Solution.CalculateLastXCoordinateProduct("task");
        Assert.Equal(107256172, result);
    }
}
