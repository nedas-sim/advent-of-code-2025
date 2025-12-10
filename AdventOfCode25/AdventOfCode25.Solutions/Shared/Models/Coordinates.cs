namespace AdventOfCode25.Solutions.Shared.Models;

public record struct Coordinates(int Row, int Column)
{
    public readonly bool IsWithinGrid<T>(Grid<T> grid)
    {
        return (Row, Column) switch
        {
            ( < 0, _) or (_, < 0) => false,
            (int row, int column) when row >= grid.RowCount || column >= grid.ColumnCount => false,
            _ => true,
        };
    }

    public readonly IEnumerable<Coordinates> EnumerateNeighborCoordinates()
    {
        yield return new(Row - 1, Column - 1);
        yield return new(Row - 1, Column);
        yield return new(Row - 1, Column + 1);

        yield return new(Row, Column - 1);
        yield return new(Row, Column + 1);

        yield return new(Row + 1, Column - 1);
        yield return new(Row + 1, Column);
        yield return new(Row + 1, Column + 1);
    }

    public readonly long GetBoundingBoxArea(Coordinates other)
    {
        long rowDiff = 1 + Diff(Row, other.Row);
        long colDiff = 1 + Diff(Column, other.Column);
        return rowDiff * colDiff;
    }

    private static long Diff(long a, long b)
    {
        if (a > b)
        {
            return a - b;
        }

        return b - a;
    }
}
