using AdventOfCode25.Solutions.Day06.Models;

namespace AdventOfCode25.Solutions.Day06;

public class Solution
{
    public static async Task<long> SumAnswersAsync(string fileName)
    {
        MathProblemCollection mathProblemCollection = new();

        await foreach ((int lineIndex, string lineInput) in File.ReadLinesAsync($"./Day06/{fileName}.txt").Index())
        {
            Action<string> actionToCall = lineIndex switch
            {
                0 => mathProblemCollection.HandleFirstLine,
                _ => mathProblemCollection.HandleOtherLine,
            };

            actionToCall(lineInput);
        }

        return mathProblemCollection.ProblemAnswerSum;
    }
}
