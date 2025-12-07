namespace AdventOfCode25.Solutions.Day07.Models;

public enum TachyonManifoldTileType
{
    Empty = 1,
    BeamEntryPoint,
    Splitter,
    Beam,
}

public static class TachyonManifoldTileTypeExtensions
{
    extension(TachyonManifoldTileType)
    {
        public static TachyonManifoldTileType Create(char c) => c switch
        {
            '.' => TachyonManifoldTileType.Empty,
            'S' => TachyonManifoldTileType.BeamEntryPoint,
            '^' => TachyonManifoldTileType.Splitter,
            '|' => TachyonManifoldTileType.Beam,
            _ => throw new Exception($"{c}? Kaip sakot?"),
        };
    }
}
