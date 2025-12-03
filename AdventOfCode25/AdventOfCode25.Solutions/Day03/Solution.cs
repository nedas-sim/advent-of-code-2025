using AdventOfCode25.Solutions.Day03.Models;

namespace AdventOfCode25.Solutions.Day03;

public class Solution
{
    public static async Task<long> SumJoltagesAsync(string fileName, int amountOfDigits = 2)
    {
        long sumOfJoltages = 0;
        
        await foreach (string inputLine in File.ReadLinesAsync($"./Day03/{fileName}.txt"))
        {
            using BatteryBank batteryBank = new(inputLine);
            sumOfJoltages += batteryBank.FindHighestJoltage(amountOfDigits);
        }

        return sumOfJoltages;
    }
}
