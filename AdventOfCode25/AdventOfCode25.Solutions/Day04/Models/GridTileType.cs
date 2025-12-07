namespace AdventOfCode25.Solutions.Day04.Models;

public enum GridTileType
{
    Empty = 1,
    PaperRoll,
    ForkliftReachable,
}

public static class GridTileTypeExtensions
{
    extension(GridTileType)
    {
        public static GridTileType Create(char c) => c switch
        {
            '.' => GridTileType.Empty,
            '@' => GridTileType.PaperRoll,
            _ => throw new Exception($"{c}? Kaip sakot?"),
        };
    }
}
