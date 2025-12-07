using AdventOfCode25.Solutions.Shared.Models;

namespace AdventOfCode25.Solutions.Day07.Models;

public class TachyonManifold(bool allowDuplicateBeams)
    : Grid<TachyonManifoldTileType>(TachyonManifoldTileType.Create)
{
    private readonly Dictionary<Coordinates, long> _cache = [];

    public long CountSplits()
    {
        int startingIndex = _rows[0].Index()
            .Where(x => x.Item == TachyonManifoldTileType.BeamEntryPoint)
            .First()
            .Index;

        return ShineBeamFurther(0, startingIndex);
    }

    private long ShineBeamFurther(int row, int beamIndex)
    {
        Coordinates coords = new(row, beamIndex);
        if (!coords.IsWithinGrid(this))
        {
            return 0;
        }

        if (allowDuplicateBeams && _cache.TryGetValue(coords, out long cachedResult))
        {
            return cachedResult;
        }

        TachyonManifoldTileType currentTile = this[coords];
        long cacheResult(long value) => CacheResultAtCoordinates(coords, value);

        if (currentTile == TachyonManifoldTileType.Beam)
        {
            return cacheResult(allowDuplicateBeams
                ? ShineBeamFurther(row + 1, beamIndex)
                : 0);
        }

        if (currentTile == TachyonManifoldTileType.Splitter)
        {
            return cacheResult(1 + ShineBeamFurther(row, beamIndex - 1) + ShineBeamFurther(row, beamIndex + 1));
        }

        this[coords] = TachyonManifoldTileType.Beam;

        return cacheResult(ShineBeamFurther(row + 1, beamIndex));
    }

    private long CacheResultAtCoordinates(Coordinates coords, long value)
    {
        _cache[coords] = value;
        return value;
    }
}
