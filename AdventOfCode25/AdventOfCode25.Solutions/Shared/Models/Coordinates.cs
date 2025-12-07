using AdventOfCode25.Solutions.Day04.Models;

namespace AdventOfCode25.Solutions.Shared.Models;

public record struct Coordinates(int Row, int Column)
{
    public readonly bool IsWithinGrid(PaperGrid grid)
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
}
