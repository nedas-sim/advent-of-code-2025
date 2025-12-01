using AdventOfCode25.Solutions.Day01.Models;

namespace AdventOfCode25.Solutions.Day01;

public class Solution<T> where T : class, IDialTurn<T>
{
    public static async Task<int> CalculateTotalZerosOnTurnEndAsync(string fileName)
    {
        int pointerAt = 50;
        int totalZeros = 0;

        await foreach (string inputLine in File.ReadLinesAsync($"./Day01/{fileName}.txt"))
        {
            T turn = T.BuildNext(inputLine);

            pointerAt = turn.Turn(pointerAt);

            if (pointerAt is 0)
            {
                totalZeros++;
            }
        }

        return totalZeros;
    }

    public static async Task<int> CalculateTotalZerosAtAnyPointAsync(string fileName)
    {
        int pointerAt = 50;
        int totalZeros = 0;

        await foreach (string inputLine in File.ReadLinesAsync($"./Day01/{fileName}.txt"))
        {
            T turn = T.BuildNext(inputLine);
            pointerAt = turn.Turn(pointerAt, out int increment);
            totalZeros += increment;
        }

        return totalZeros;
    }
}
