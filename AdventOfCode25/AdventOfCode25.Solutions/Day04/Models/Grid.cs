namespace AdventOfCode25.Solutions.Day04.Models;

public class Grid(bool canForkliftGoOverReachablePapers = false)
{
    private readonly List<List<GridTile>> _rows = [];

    public int RowCount => _rows.Count;
    public int ColumnCount => _rows[0].Count;

    public void AddRow(string row)
    {
        List<GridTile> tiles = [.. row
            .Select(tile => tile switch
            {
                '.' => GridTileType.Empty,
                '@' => GridTileType.PaperRoll,
                _ => throw new Exception($"{tile}? Kaip sakot?"),
            })
            .Select(tileType => new GridTile
            {
                Type = tileType,
            })];

        _rows.Add(tiles);
    }

    public bool SetForkliftAccessibleTiles(int maxAmountOfNeighborPapers)
    {
        bool stompedOnce = false;

        for (int row = 0; row < RowCount; row++)
        {
            for (int column = 0; column < ColumnCount; column++)
            {
                Coordinates coordinates = new(row, column);

                GridTile tileOfInterest = GetTileByCoordinates(coordinates);

                if (tileOfInterest.Type != GridTileType.PaperRoll)
                {
                    continue;
                }

                bool isForkliftReachable = EnumerateNeighborCoordinates(coordinates)
                    .Where(coords => coords.IsWithinGrid(this))
                    .Select(GetTileByCoordinates)
                    .Where(IsUnstompableTile)
                    .Count() < maxAmountOfNeighborPapers;

                if (isForkliftReachable)
                {
                    tileOfInterest.Type = GridTileType.ForkliftReachable;
                    stompedOnce = true;
                }
            }
        }

        return stompedOnce;
    }

    private bool IsUnstompableTile(GridTile tile)
    {
        return canForkliftGoOverReachablePapers
            ? tile.Type == GridTileType.PaperRoll
            : tile.Type != GridTileType.Empty;
    }

    public int ForkliftAccessibleTileCount => _rows
        .SelectMany(x => x)
        .Where(x => x.Type == GridTileType.ForkliftReachable)
        .Count();

    private GridTile GetTileByCoordinates(Coordinates coordinates)
        => _rows[coordinates.Row][coordinates.Column];

    private static IEnumerable<Coordinates> EnumerateNeighborCoordinates(Coordinates center)
    {
        yield return new(center.Row - 1, center.Column - 1);
        yield return new(center.Row - 1, center.Column);
        yield return new(center.Row - 1, center.Column + 1);

        yield return new(center.Row, center.Column - 1);
        yield return new(center.Row, center.Column + 1);

        yield return new(center.Row + 1, center.Column - 1);
        yield return new(center.Row + 1, center.Column);
        yield return new(center.Row + 1, center.Column + 1);
    }
}

public class GridTile
{
    public GridTileType Type { get; set; }
}

public enum GridTileType
{
    Empty = 1,
    PaperRoll,
    ForkliftReachable,
}

public record struct Coordinates(int Row, int Column)
{
    public readonly bool IsWithinGrid(Grid grid)
    {
        return (Row, Column) switch
        {
            ( < 0, _) or (_, < 0) => false,
            (int row, int column) when row >= grid.RowCount || column >= grid.ColumnCount => false,
            _ => true,
        };
    }
}
