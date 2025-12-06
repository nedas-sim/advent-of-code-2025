using AdventOfCode25.Solutions.Shared;

namespace AdventOfCode25.Solutions.Day06.Models;

public class MathProblemCollection<T>
    where T : MathProblem, new()
{
    private readonly List<MathProblem> _mathProblemList;
    private readonly List<Range> _numberRanges;

    public long ProblemAnswerSum => _mathProblemList.Select(x => x.Calculate()).Sum();

    public MathProblemCollection(string operationInputLine)
    {
        List<int> operationIndices =
        [
            ..operationInputLine.Index().Where(x => x.Item != ' ').Select(x => x.Index),
            operationInputLine.Length,
        ];

        _numberRanges =
        [
            .. operationIndices[..^1]
                .Zip(operationIndices[1..])
                .Select(x => new Range(x.First, x.Second)),
        ];

        IEnumerable<T> mathProblems = Utils.SplitBySpaces<MathOperationWrapper>(operationInputLine)
            .Select(wrapper =>
            {
                T mathProblem = new();
                mathProblem.SetOperation(wrapper.Operation);
                return mathProblem;
            });

        _mathProblemList = [.. mathProblems];
    }

    public void HandleNumberLine(string inputLine)
    {
        foreach ((int index, Range range) in _numberRanges.Index())
        {
            string number = inputLine[range];
            _mathProblemList[index].AppendValue(number);
        }
    }
}