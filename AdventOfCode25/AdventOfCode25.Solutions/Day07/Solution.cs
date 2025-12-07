using AdventOfCode25.Solutions.Day07.Models;

namespace AdventOfCode25.Solutions.Day07;

public class Solution
{
    public static async Task<long> CountSplits(string fileName, bool allowDuplicateBeams)
    {
        TachyonManifold map = new(allowDuplicateBeams);
        await map.LoadFromFileAsync($"./Day07/{fileName}.txt");

        return map.CountSplits()
            + (allowDuplicateBeams ? 1 : 0); // whether or not to account for starting point
    }
}
