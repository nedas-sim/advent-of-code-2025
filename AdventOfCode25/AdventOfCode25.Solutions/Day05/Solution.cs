using AdventOfCode25.Solutions.Day05.Models;

namespace AdventOfCode25.Solutions.Day05;

public class Solution
{
    public static async Task<int> CountFreshIdsAsync(string fileName)
    {
        FreshIdRangeCollection freshIdRangeCollection = new();
        bool reachedBreak = false;

        int totalFreshIds = 0;

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

                    if (freshIdRangeCollection.IsInAnyRange(numberToCheck))
                    {
                        totalFreshIds++;
                    }

                    break;
                }
            };
        }

        return totalFreshIds;
    }
}
