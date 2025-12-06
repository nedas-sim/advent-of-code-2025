using AdventOfCode25.Solutions.Day06.Models;

namespace AdventOfCode25.Solutions.Day06;

public class Solution
{
    public static async Task<long> SumAnswersAsync<T>(string fileName) where T : MathProblem, new()
    {
        List<string> lines = [.. File.ReadLines($"./Day06/{fileName}.txt")];
        List<string> numberLines = lines[..(^1)];

        MathProblemCollection<T> mathProblemCollection = new(lines.Last());

        foreach (string numberLine in numberLines)
        {
            mathProblemCollection.HandleNumberLine(numberLine);
        }

        return mathProblemCollection.ProblemAnswerSum;
    }
}
