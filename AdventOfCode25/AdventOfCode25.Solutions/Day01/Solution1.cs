using AdventOfCode25.Solutions.Day01.Models;

namespace AdventOfCode25.Solutions.Day01;

public class Solution1
{
    public static async Task<int> CalculateTotalZerosAsync(string fileName)
    {
        int pointerAt = 50;
        int totalZeros = 0;

        await foreach (string inputLine in File.ReadLinesAsync($"./Day01/{fileName}.txt"))
        {
            DialTurn turn = new(inputLine);
            pointerAt = turn.Turn(pointerAt);

            if (pointerAt is 0)
            {
                totalZeros++;
            }
        }

        return totalZeros;
    }
}
