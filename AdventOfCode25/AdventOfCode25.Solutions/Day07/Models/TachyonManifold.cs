using AdventOfCode25.Solutions.Shared.Models;

namespace AdventOfCode25.Solutions.Day07.Models;

public class TachyonManifold()
    : Grid<TachyonManifoldTileType>(TachyonManifoldTileType.Create)
{
    public int CountSplits()
    {
        int startingIndex = _rows[0].Index()
            .Where(x => x.Item == TachyonManifoldTileType.BeamEntryPoint)
            .First()
            .Index;

        return ShineBeamFurther(0, startingIndex);
    }

    private int ShineBeamFurther(int row, int beamIndex)
    {
        Coordinates coords = new(row, beamIndex);
        if (!coords.IsWithinGrid(this))
        {
            return 0;
        }

        TachyonManifoldTileType currentTile = this[coords];

        if (currentTile == TachyonManifoldTileType.Beam)
        {
            return 0;
        }

        if (currentTile == TachyonManifoldTileType.Splitter)
        {
            return 1 + ShineBeamFurther(row, beamIndex - 1) + ShineBeamFurther(row, beamIndex + 1);
        }

        this[coords] = TachyonManifoldTileType.Beam;
        return ShineBeamFurther(row + 1, beamIndex);
    }
}
