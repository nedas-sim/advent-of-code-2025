using AdventOfCode25.Solutions.Shared.Models;

namespace AdventOfCode25.Solutions.Day04.Models;

public class PaperGrid(bool canForkliftGoOverReachablePapers = false)
    : Grid<GridTileType>(GridTileType.Create)
{
    public bool SetForkliftAccessibleTiles(int maxAmountOfNeighborPapers)
    {
        bool stompedOnce = false;

        for (int row = 0; row < RowCount; row++)
        {
            for (int column = 0; column < ColumnCount; column++)
            {
                Coordinates coordinates = new(row, column);

                GridTileType tileOfInterest = this[coordinates];

                if (tileOfInterest != GridTileType.PaperRoll)
                {
                    continue;
                }

                bool isForkliftReachable = coordinates.EnumerateNeighborCoordinates()
                    .Where(coords => coords.IsWithinGrid(this))
                    .Select(x => this[x])
                    .Where(IsUnstompableTile)
                    .Count() < maxAmountOfNeighborPapers;

                if (isForkliftReachable)
                {
                    this[coordinates] = GridTileType.ForkliftReachable;
                    stompedOnce = true;
                }
            }
        }

        return stompedOnce;
    }

    private bool IsUnstompableTile(GridTileType tileType)
    {
        return canForkliftGoOverReachablePapers
            ? tileType == GridTileType.PaperRoll
            : tileType != GridTileType.Empty;
    }

    public int ForkliftAccessibleTileCount => _rows
        .SelectMany(x => x)
        .Where(x => x == GridTileType.ForkliftReachable)
        .Count();
}
