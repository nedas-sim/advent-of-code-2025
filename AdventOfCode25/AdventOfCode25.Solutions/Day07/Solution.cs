using AdventOfCode25.Solutions.Day07.Models;

namespace AdventOfCode25.Solutions.Day07;

public class Solution
{
    public static async Task<int> CountSplits(string fileName)
    {
        TachyonManifold map = new();
        await map.LoadFromFileAsync($"./Day07/{fileName}.txt");
        return map.CountSplits();
    }
}
