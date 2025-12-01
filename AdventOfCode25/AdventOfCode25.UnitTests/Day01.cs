using AdventOfCode25.Solutions.Day01;
using AdventOfCode25.Solutions.Day01.Models;

namespace AdventOfCode25.UnitTests;

public class Day01
{
    [Fact]
    public async Task TestSolution1()
    {
        int totalZeros = await Solution.CalculateTotalZerosOnTurnEndAsync("sample");
        Assert.Equal(3, totalZeros);
    }

    [Fact]
    public async Task RunSolution1()
    {
        int totalZeros = await Solution.CalculateTotalZerosOnTurnEndAsync("task");
        Assert.Equal(980, totalZeros);
    }

    [Fact]
    public async Task TestSolution2()
    {
        int totalZeros = await Solution.CalculateTotalZerosAtAnyPointAsync("sample");
        Assert.Equal(6, totalZeros);
    }

    [Fact]
    public async Task RunSolution2()
    {
        int totalZeros = await Solution.CalculateTotalZerosAtAnyPointAsync("task");
        Assert.Equal(5961, totalZeros);
    }

    [Theory]
    [InlineData("L10", 5, 95)]
    [InlineData("R10", 95, 5)]
    [InlineData("R5", 95, 0)]
    [InlineData("R4", 95, 99)]
    [InlineData("L4", 0, 96)]
    public void DialTurn_Turn_ShouldGetExpected(string inputLine, int currentValue, int nextExpectedValue)
    {
        // Arrange
        DialTurn dialturn = new(inputLine);

        // Act
        int newPointerAt = dialturn.Turn(currentValue);

        // Assert
        Assert.Equal(nextExpectedValue, newPointerAt);
    }

    [Theory]
    [InlineData("L10", 5, 95, 1)]
    [InlineData("R10", 95, 5, 1)]
    [InlineData("R5", 95, 0, 1)]
    [InlineData("R4", 95, 99, 0)]
    [InlineData("L4", 0, 96, 0)]
    [InlineData("R949", 50, 99, 9)]
    [InlineData("R950", 50, 0, 10)]
    [InlineData("R1000", 50, 50, 10)]
    [InlineData("R100", 0, 0, 1)]
    public void DialTurn_TurnWithPassTracking_ShouldGetExpected(string inputLine, int currentValue, int nextExpectedValue, int passesExpected)
    {
        // Arrange
        DialTurn dialturn = new(inputLine);

        // Act
        int newPointerAt = dialturn.Turn(currentValue, out int passes);

        // Assert
        Assert.Equal(nextExpectedValue, newPointerAt);
        Assert.Equal(passesExpected, passes);
    }
}
