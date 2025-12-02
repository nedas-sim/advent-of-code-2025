using AdventOfCode25.Solutions.Day02;
using AdventOfCode25.Solutions.Day02.Models;

namespace AdventOfCode25.UnitTests;

public class Day02
{
    [Fact]
    public async Task TestSolution1()
    {
        long invalidIdSum = await Solution.CalculateInvalidIdSumAsync("sample");
        Assert.Equal(1227775554, invalidIdSum);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long invalidIdSum = await Solution.CalculateInvalidIdSumAsync("task");
        Assert.Equal(24747430309, invalidIdSum);
    }

    [Theory]
    [InlineData("11-22", new long[] { 11, 22 })]
    [InlineData("95-115", new long[] { 99 })]
    [InlineData("998-1012", new long[] { 1010 })]
    [InlineData("1188511880-1188511890", new long[] { 1188511885 })]
    [InlineData("222220-222224", new long[] { 222222 })]
    [InlineData("1698522-1698528", new long[] { })]
    [InlineData("446443-446449", new long[] { 446446 })]
    [InlineData("38593856-38593862", new long[] { 38593859 })]
    public void ProductIdRange_GetInvalidIds_ReturnsExpectedCollection(string inputLine, long[] expectedInvalidIds)
    {
        // Arrange
        ProductIdRange range = new(inputLine);

        // Act
        IEnumerable<long> invalidIds = range.GetInvalidIds();

        // Assert
        Assert.True(invalidIds.SequenceEqual(expectedInvalidIds));
    }
}
