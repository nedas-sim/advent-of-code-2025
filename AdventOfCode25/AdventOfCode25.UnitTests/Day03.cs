using AdventOfCode25.Solutions.Day03;
using AdventOfCode25.Solutions.Day03.Models;

namespace AdventOfCode25.UnitTests;

public class Day03
{
    [Fact]
    public async Task TestSolution1()
    {
        long sum = await Solution.SumJoltagesAsync("sample");
        Assert.Equal(357, sum);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long sum = await Solution.SumJoltagesAsync("task");
        Assert.Equal(17031, sum);
    }

    [Fact]
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
    }

    [Theory]
    [InlineData("987654321111111", 98)]
    [InlineData("811111111111119", 89)]
    [InlineData("234234234234278", 78)]
    [InlineData("818181911112111", 92)]
    public void BatteryBank_FindHighestJoltageOfLengthTwo_ShouldReturnExpected(string inputLine, int expected)
    {
        // Arrange
        BatteryBank batteryBank = new(inputLine);

        // Act
        long actual = batteryBank.FindHighestJoltage(2);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("987654321111111", 987)]
    [InlineData("811111111111119", 819)]
    [InlineData("234234234234278", 478)]
    [InlineData("818181911112111", 921)]
    public void BatteryBank_FindHighestJoltageOfLengthThree_ShouldReturnExpected(string inputLine, int expected)
    {
        // Arrange
        BatteryBank batteryBank = new(inputLine);

        // Act
        long actual = batteryBank.FindHighestJoltage(3);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("818181911112111", "9", "11112111")]
    [InlineData("987654321111111", "9", "87654321111111")]
    [InlineData("234234234234278", "9", null)]
    [InlineData("78", "8", "")]
    public void BatteryBank_GetStringTail_ShouldReturnExpected(string input, string toFind, string? expected)
    {
        // Act
        string? actual = BatteryBank.GetStringTail(input, toFind);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void LinqOfType_WithNullablePart_ShouldFilterOut()
    {
        IEnumerable<(int, string?)> enumerable = [
            (1, "some value"),
            (1, null),
        ];

        IEnumerable<(int, string)> filtered = enumerable
            .Where(x => x.Item2 is not null)                // <-- NEED THIS :(
            .OfType<(int, string)>();

        Assert.Single(filtered);
    }
}
