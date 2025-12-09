using AdventOfCode25.Solutions.Shared;
using AdventOfCode25.Solutions.Shared.Models;

namespace AdventOfCode25.Solutions.Day09;

public class Solution
{
    public static async Task<long> GetLargestSquareAreaAsync(string fileName)
    {
        long maxArea = File.ReadLines($"./Day09/{fileName}.txt")
            .Select(x => Utils.SplitByComma<int>(x).ToArray())
            .Select(x => new Coordinates(x[0], x[1]))
            .GetUniqueCombinations()
            .Select(x => x.Item1.GetBoundingBoxArea(x.Item2))
            .Max();

        return maxArea;
    }
}
