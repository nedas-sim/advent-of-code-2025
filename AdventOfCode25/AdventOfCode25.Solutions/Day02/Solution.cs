
using AdventOfCode25.Solutions.Day02.Models;

namespace AdventOfCode25.Solutions.Day02;

public class Solution
{
    public static async Task<long> CalculateInvalidIdSumAsync(string fileName)
    {
        string fileText = await File.ReadAllTextAsync($"./Day02/{fileName}.txt");
        string[] productIdRanges = fileText.Split(',');

        long invalidIdSum = productIdRanges
            .Select(x => new ProductIdRange(x))
            .SelectMany(x => x.GetInvalidIds())
            .Sum();

        return invalidIdSum;
    }
}
