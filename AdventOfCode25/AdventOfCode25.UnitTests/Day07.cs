using AdventOfCode25.Solutions.Day07;

namespace AdventOfCode25.UnitTests;

public class Day07
{
    [Fact]
    public async Task TestSolution1()
    {
        int splits = await Solution.CountSplits("sample");
        Assert.Equal(21, splits);
    }

    [Fact]
    public async Task RunSolution1()
    {
        int splits = await Solution.CountSplits("task");
        Assert.Equal(1619, splits);
    }

    /*[Fact]
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
    }*/
}
