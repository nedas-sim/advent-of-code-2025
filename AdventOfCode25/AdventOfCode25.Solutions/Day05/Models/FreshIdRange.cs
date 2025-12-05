using AdventOfCode25.Solutions.Shared;

namespace AdventOfCode25.Solutions.Day05.Models;

public class FreshIdRange
{
    private readonly long _start, _end;

    public FreshIdRange(string inputLine)
    {
        long[] splitLine = Utils.SplitByDash<long>(inputLine);
        (_start, _end) = (splitLine[0], splitLine[1]);
    }

    public bool IsInRange(long number)
    {
        return _start <= number && number <= _end;
    }
}

public class FreshIdRangeCollection
{
    private readonly List<FreshIdRange> _ranges = [];

    public void AddRangeFromInputLine(string inputLine)
    {
        _ranges.Add(new FreshIdRange(inputLine));
    }

    public bool IsInAnyRange(long number)
    {
        return _ranges.Any(x => x.IsInRange(number));
    }
}