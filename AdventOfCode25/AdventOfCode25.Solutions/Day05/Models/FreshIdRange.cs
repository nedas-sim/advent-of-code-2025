using AdventOfCode25.Solutions.Shared;

namespace AdventOfCode25.Solutions.Day05.Models;

public class FreshIdRange
{
    public long Start { get; private set; }
    public long End { get; private set; }

    public long Length => End - Start + 1;

    public FreshIdRange(string inputLine)
    {
        long[] splitLine = Utils.SplitByDash<long>(inputLine);
        (Start, End) = (splitLine[0], splitLine[1]);
    }

    private FreshIdRange() { }

    public bool IsInRange(long number)
    {
        return Start <= number && number <= End;
    }

    public static FreshIdRange? CombineOverlappingRanges(FreshIdRange one, FreshIdRange two)
    {
        if (one.Start <= two.Start && one.End >= two.End)
        {
            return one;
        }

        if (one.End + 1 < two.Start)
        {
            return null;
        }

        return new FreshIdRange
        {
            Start = one.Start,
            End = two.End,
        };
    }
}

public class FreshIdRangeCollection : List<FreshIdRange>
{
    public void AddRangeFromInputLine(string inputLine)
    {
        Add(new FreshIdRange(inputLine));
    }

    public bool IsInAnyRange(long number)
    {
        return this.Any(x => x.IsInRange(number));
    }

    public bool TryCombineRanges(out FreshIdRangeCollection collection)
    {
        List<FreshIdRange> ordered = [.. this.OrderBy(x => x.Start)];

        collection = [ ordered[0] ];
        int indexOfInterest = 0;

        bool combinedSomething = false;

        for (int i = 1; i < Count; i++)
        {
            FreshIdRange rangeOfInterest = collection[indexOfInterest];
            
            FreshIdRange? combined = FreshIdRange.CombineOverlappingRanges(rangeOfInterest, ordered[i]);

            if (combined is not null)
            {
                combinedSomething = true;
                collection[indexOfInterest] = combined;
                continue;
            }

            indexOfInterest++;
            collection.Add(ordered[i]);
        }

        return combinedSomething;
    }
}