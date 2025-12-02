using AdventOfCode25.Solutions.Day02;
using AdventOfCode25.Solutions.Day02.Models;

namespace AdventOfCode25.UnitTests;

public class Day02
{
    [Fact]
    public async Task TestSolution1()
    {
        long invalidIdSum = await Solution.CalculateInvalidIdAsSplitInHalfSumAsync("sample");
        Assert.Equal(1227775554, invalidIdSum);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long invalidIdSum = await Solution.CalculateInvalidIdAsSplitInHalfSumAsync("task");
        Assert.Equal(24747430309, invalidIdSum);
    }

    [Fact]
    public async Task TestSolution2()
    {
        long invalidIdSum = await Solution.CalculateInvalidIdSumAsync("sample");
        Assert.Equal(4174379265, invalidIdSum);
    }

    [Fact]
    public async Task RunSolution2()
    {
        long invalidIdSum = await Solution.CalculateInvalidIdSumAsync("task");
        Assert.Equal(30962646823, invalidIdSum);
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
    public void ProductIdRange_GetInvalidIds_WhenCheckingRepeatTwice_ReturnsExpectedCollection(string inputLine, long[] expectedInvalidIds)
    {
        // Arrange
        ProductIdRange range = new(inputLine);

        // Act
        IEnumerable<long> invalidIds = range.GetInvalidIds(2);

        // Assert
        Assert.True(invalidIds.SequenceEqual(expectedInvalidIds));
    }

    [Theory]
    [InlineData("11-22", new long[] { 11, 22 })]
    [InlineData("95-115", new long[] { 99, 111 })]
    [InlineData("998-1012", new long[] { 999, 1010 })]
    [InlineData("1188511880-1188511890", new long[] { 1188511885 })]
    [InlineData("222220-222224", new long[] { 222222 })]
    [InlineData("1698522-1698528", new long[] { })]
    [InlineData("446443-446449", new long[] { 446446 })]
    [InlineData("38593856-38593862", new long[] { 38593859 })]
    [InlineData("565653-565659", new long[] { 565656 })]
    [InlineData("824824821-824824827", new long[] { 824824824 })]
    [InlineData("2121212118-2121212124", new long[] { 2121212121 })]
    public void ProductIdRange_GetInvalidIds_WhenCheckingRepeatsOfAnyLength_ReturnsExpectedCollection(string inputLine, long[] expectedInvalidIds)
    {
        // Arrange
        ProductIdRange range = new(inputLine);

        // Act
        IEnumerable<long> invalidIds = range.GetInvalidIds();

        // Assert
        Assert.True(invalidIds.SequenceEqual(expectedInvalidIds));
    }

    [Theory]
    [InlineData("11", "1", true)]
    [InlineData("12", "1", false)]
    [InlineData("1212", "12", true)]
    [InlineData("12121", "12", false)]
    public void ProductIdRange_IsRepeating_ReturnsExpectedValue(string valueToCheck, string suffix, bool expectedResult)
    {
        bool actual = ProductIdRange.IsRepeating(valueToCheck, suffix);

        Assert.Equal(expectedResult, actual);
    }

    [Theory]
    [InlineData("11", 1, true)]
    [InlineData("1212", 2, true)]
    [InlineData("12121", 2, false)]
    public void ProductIdRange_IsInvalidId_ReturnsExpectedValue(string valueToCheck, int suffixLength, bool expectedResult)
    {
        bool actual = ProductIdRange.IsInvalidId(valueToCheck, suffixLength);

        Assert.Equal(expectedResult, actual);
    }
}
