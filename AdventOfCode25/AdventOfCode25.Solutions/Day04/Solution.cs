using AdventOfCode25.Solutions.Day04.Models;

namespace AdventOfCode25.Solutions.Day04;

public class Solution
{
    public static async Task<int> CountForkliftAccessibleTilesAsync(string fileName, bool canStompReachablePapers)
    {
        PaperGrid grid = new(canStompReachablePapers);

        await foreach (string inputLine in File.ReadLinesAsync($"./Day04/{fileName}.txt"))
        {
            grid.AddRow(inputLine);
        }

        grid.SetForkliftAccessibleTiles(4);

        if (canStompReachablePapers)
        {
            bool stompedSomething;

            do
            {
                stompedSomething = grid.SetForkliftAccessibleTiles(4);
            }
            while (stompedSomething);
        }

        return grid.ForkliftAccessibleTileCount;
    }
}