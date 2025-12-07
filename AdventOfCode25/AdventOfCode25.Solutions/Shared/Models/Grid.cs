namespace AdventOfCode25.Solutions.Shared.Models;

public abstract class Grid<T>(Func<char, T> tileMapFunc)
{
    protected readonly List<List<T>> _rows = [];

    public int RowCount => _rows.Count;
    public int ColumnCount => _rows[0].Count;

    public void AddRow(string row)
    {
        List<T> tiles = [ ..row.Select(tileMapFunc) ];
        _rows.Add(tiles);
    }

    protected T this[Coordinates coordinates]
    {
        get => _rows[coordinates.Row][coordinates.Column];
        set => _rows[coordinates.Row][coordinates.Column] = value;
    }
}
