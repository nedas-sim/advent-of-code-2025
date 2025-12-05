using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode25.Solutions.Day05.Models;

public interface ICheckRange
{
    void SetSample(long sample);
    long CountFreshIds(FreshIdRangeCollection collection);
}

public class RangeOfIndividualSamples : ICheckRange
{
    private readonly List<long> _samples = [];

    public long CountFreshIds(FreshIdRangeCollection collection)
    {
        return _samples.Where(x => collection.IsInAnyRange(x)).Count();
    }

    public void SetSample(long sample)
    {
        _samples.Add(sample);
    }
}

public class RangeOfMinMaxValues : ICheckRange
{
    public long CountFreshIds(FreshIdRangeCollection collection)
    {
        while (collection.TryCombineRanges(out collection)) { }

        return collection.Select(x => x.Length).Sum();
    }

    public void SetSample(long sample)
    {
        // nothing on purpose
    }
}
