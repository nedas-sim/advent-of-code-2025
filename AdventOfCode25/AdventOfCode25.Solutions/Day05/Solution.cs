using AdventOfCode25.Solutions.Day05.Models;

namespace AdventOfCode25.Solutions.Day05;

public class Solution
{
    public static async Task<long> CountFreshIdsAsync<T>(string fileName)
        where T : ICheckRange, new()
    {
        FreshIdRangeCollection freshIdRangeCollection = new();
        bool reachedBreak = false;

        T checkRange = new();

        await foreach (string inputLine in File.ReadLinesAsync($"./Day05/{fileName}.txt"))
        {
            switch (reachedBreak, inputLine)
            {
                case (false, ""):
                {
                    reachedBreak = true;
                    break;
                }

                case (false, _):
                {
                    freshIdRangeCollection.AddRangeFromInputLine(inputLine);
                    break;
                }

                default:
                {
                    long numberToCheck = long.Parse(inputLine);

                    checkRange.SetSample(numberToCheck);

                    break;
                }
            };
        }

        return checkRange.CountFreshIds(freshIdRangeCollection);
    }
}
