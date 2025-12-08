using AdventOfCode25.Solutions.Day08.Models;

namespace AdventOfCode25.Solutions.Day08;

public class Solution
{
    public static async Task<long> CalculateDistanceProductAsync(string fileName, int amountOfConnections)
    {
        ThreeDimensionalSpace space = new();

        await foreach (string inputLine in File.ReadLinesAsync($"./Day08/{fileName}.txt"))
        {
            space.AddCoordinate(inputLine);
        }

        return space.CalculateWhatIsNeeded(amountOfConnections);
    }
}
