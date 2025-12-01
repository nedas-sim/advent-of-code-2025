using AdventOfCode25.Solutions.Day01;
using AdventOfCode25.Solutions.Day01.Models;

namespace AdventOfCode25.UnitTests;

public class Day01
{
    [Fact]
    public async Task TestSolution1()
    {
        int totalZeros = await Solution1.CalculateTotalZerosAsync("sample");
        Assert.Equal(3, totalZeros);
    }

    [Fact]
    public async Task RunSolution1()
    {
        int totalZeros = await Solution1.CalculateTotalZerosAsync("task1");
        Assert.Equal(980, totalZeros);
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
}
