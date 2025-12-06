using AdventOfCode25.Solutions.Day06;
using AdventOfCode25.Solutions.Day06.Models;

namespace AdventOfCode25.UnitTests;

public class Day06
{
    [Fact]
    public async Task TestSolution1()
    {
        long answerSum = await Solution.SumAnswersAsync<HumanMathProblem>("sample");
        Assert.Equal(4277556, answerSum);
    }

    [Fact]
    public async Task RunSolution1()
    {
        long answerSum = await Solution.SumAnswersAsync<HumanMathProblem>("task");
        Assert.Equal(4693419406682, answerSum);
    }

    [Fact]
    public async Task TestSolution2()
    {
        long answerSum = await Solution.SumAnswersAsync<CephalopodMathProblem>("sample");
        Assert.Equal(3263827, answerSum);
    }

    [Fact]
    public async Task RunSolution2()
    {
        long answerSum = await Solution.SumAnswersAsync<CephalopodMathProblem>("task");
        Assert.Equal(9029931401920, answerSum);
    }
}
