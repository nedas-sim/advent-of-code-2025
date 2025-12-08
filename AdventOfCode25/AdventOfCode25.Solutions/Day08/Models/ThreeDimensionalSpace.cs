using AdventOfCode25.Solutions.Shared;

namespace AdventOfCode25.Solutions.Day08.Models;

public abstract class ThreeDimensionalSpace(bool performLoopActionAtStart)
{
    protected readonly List<ThreeDimensionCoordinate> _coordinates = [];

    protected ThreeDimensionCoordinate this[int index]
    {
        get => _coordinates[index];
    }

    public void AddCoordinate(string inputLine)
    {
        int[] values = [.. Utils.SplitByComma<int>(inputLine)];
        ThreeDimensionCoordinate coordinate = new(values[0], values[1], values[2]);
        _coordinates.Add(coordinate);
    }

    public long CalculateWhatIsNeeded()
    {
        SortedList<long, (int Index1, int Index2)> sortedList = [];

        for (int i = 0; i < _coordinates.Count - 1; i++)
        {
            for (int j = i + 1; j < _coordinates.Count; j++)
            {
                long distanceSq = this[i].DistanceSquared(this[j]);
                sortedList.Add(distanceSq, (i, j));
            }
        }

        List<Circuit> circuits = Enumerable.Range(0, _coordinates.Count)
            .Select(index => new Circuit(index))
            .ToList();

        foreach ((int index1, int index2) in sortedList.Values)
        {
            if (performLoopActionAtStart)
            {
                ActionOnLoop(index1, index2);

                if (ShouldLoopBreak(circuits))
                {
                    break;
                }
            }

            Circuit circuit1 = circuits.First(x => x.HasIndex(index1));
            Circuit circuit2 = circuits.First(x => x.HasIndex(index2));

            if (circuit1 == circuit2)
            {
                continue;
            }

            circuit1.CombineCircuit(circuit2);
            circuits.Remove(circuit2);

            if (!performLoopActionAtStart)
            {
                ActionOnLoop(index1, index2);

                if (ShouldLoopBreak(circuits))
                {
                    break;
                }
            }
        }

        return GetFinalResult(circuits);
    }

    protected abstract void ActionOnLoop(int index1, int index2);

    protected abstract bool ShouldLoopBreak(List<Circuit> circuits);

    protected abstract long GetFinalResult(List<Circuit> circuits);
}

public class LimitedConnectionSpace(int amountOfConnections) : ThreeDimensionalSpace(true)
{
    protected override void ActionOnLoop(int index1, int index2)
    {
        amountOfConnections--;
    }

    protected override bool ShouldLoopBreak(List<Circuit> circuits)
    {
        return amountOfConnections < 0;
    }

    protected override long GetFinalResult(List<Circuit> circuits)
    {
        return circuits.OrderByDescending(x => x.IndexCount)
            .Take(3)
            .Select(x => x.IndexCount)
            .Product();
    }
}

public class UntilLastNeededConnection() : ThreeDimensionalSpace(false)
{
    private int _lastIndex1, _lastIndex2;

    protected override void ActionOnLoop(int index1, int index2)
    {
        (_lastIndex1, _lastIndex2) = (index1, index2);
    }

    protected override bool ShouldLoopBreak(List<Circuit> circuits)
    {
        return circuits.Count == 1;
    }

    protected override long GetFinalResult(List<Circuit> circuits)
    {
        return this[_lastIndex1].X * this[_lastIndex2].X;
    }
}
