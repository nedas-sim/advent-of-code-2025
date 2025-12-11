using AdventOfCode25.Solutions.Day09.Models;
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

    public static async Task<long> GetLargestContainedRectangleAreaAsync(string fileName, bool onLeft)
    {
        List<Coordinates> coords = File.ReadLines($"./Day09/{fileName}.txt")
            .Select(x => Utils.SplitByComma<int>(x).ToArray())
            .Select(x => new Coordinates(x[1], x[0]))
            .ToList();

        LineCollection collection = new(onLeft);
        collection.AddLine(coords.Last(), coords.First());

        for (int i = 0; i < coords.Count - 1; i++)
        {
            collection.AddLine(coords[i], coords[i + 1]);
        }

        collection.AddAllBorders();

        return coords
            .GetUniqueCombinations()
            .Select(x => new Rectangle(x.Item1, x.Item2))
            .Where(x => !x.AnyBorderIntersects(collection))
            .Select(x => x.Area)
            .Max();
    }
}
