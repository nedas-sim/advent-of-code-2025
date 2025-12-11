using AdventOfCode25.Solutions.Shared.Models;
using AdventOfCode25.Solutions.Shared;

namespace AdventOfCode25.Solutions.Day09.Models;

public record Line(Coordinates Start, Coordinates End)
{
    public bool IsHorizontal => Start.Row == End.Row;
    public bool PointingInPositiveDirection
        => (End.Row - Start.Row) + (End.Column - Start.Column) > 0;

    public long Length => Utils.Diff(Start.Row, End.Row) + Utils.Diff(Start.Column, End.Column) - 1;
}

public class LineCollection(bool onLeft)
{
    private readonly HashSet<Coordinates> _coordinates = [];
    private readonly List<Coordinates> _borderCorners = [];

    private readonly List<Line> _lines = [];

    public List<Line> BorderLines { get; private set; } = [];

    public void AddLine(Coordinates start, Coordinates end)
    {
        _lines.Add(new(start, end));

        _coordinates.Add(start);
        _coordinates.Add(end);
    }

    public void AddAllBorders()
    {
        for (int i = 0; i < _lines.Count - 1; i++)
        {
            _borderCorners.Add(GetBorderCorner(_lines[i], _lines[i + 1], onLeft));
        }
        _borderCorners.Add(GetBorderCorner(_lines[^1], _lines[0], onLeft));

        for (int i = 0; i < _borderCorners.Count - 1; i++)
        {
            BorderLines.Add(new Line(_borderCorners[i], _borderCorners[i + 1]));
        }
        BorderLines.Add(new Line(_borderCorners[^1], _borderCorners[0]));
    }

    private static Coordinates GetBorderCorner(Line one, Line two, bool onLeft)
    {
        int dColumn = 0, dRow = 0;

        if (one.IsHorizontal)
        {
            (dColumn, dRow) = (one.PointingInPositiveDirection, two.PointingInPositiveDirection) switch
            {
                (true, true) => (1, -1),
                (true, false) => (-1, -1),
                (false, true) => (1, 1),
                (false, false) => (-1, 1),
            };
        }
        else
        {
            (dColumn, dRow) = (one.PointingInPositiveDirection, two.PointingInPositiveDirection) switch
            {
                (true, true) => (1, -1),
                (true, false) => (1, 1),
                (false, true) => (-1, -1),
                (false, false) => (-1, 1),
            };
        }

        if (!onLeft)
        {
            dColumn *= -1;
            dRow *= -1;
        }

        return new Coordinates(one.End.Row + dRow, one.End.Column + dColumn);
    }
}

public class Rectangle
{
    public Coordinates TopLeft { get; private set; }
    public Coordinates TopRight { get; private set; }
    public Coordinates BottomLeft { get; private set; }
    public Coordinates BottomRight { get; private set; }

    public long Area => TopLeft.GetBoundingBoxArea(BottomRight);

    public Rectangle(Coordinates one, Coordinates two)
    {
        (Coordinates left, Coordinates right) = one.Column < two.Column
            ? (one, two)
            : (two, one);

        (Coordinates top, Coordinates bottom) = one.Row < two.Row
            ? (one, two)
            : (two, one);

        TopLeft = new(top.Row, left.Column);
        TopRight = new(top.Row, right.Column);
        BottomLeft = new(bottom.Row, left.Column);
        BottomRight = new(bottom.Row, right.Column);
    }

    public bool AnyBorderIntersects(LineCollection lineCollection)
    {
        return lineCollection
            .BorderLines
            .Where(IntersectsLine)
            .Any();
    }

    private bool IntersectsLine(Line line)
    {
        return line.IsHorizontal
            ? IntersectsHorizontalLine(line)
            : IntersectsVerticalLine(line);
    }

    private bool IntersectsHorizontalLine(Line line)
    {
        bool isRowWithinRectangle = TopLeft.Row <= line.Start.Row && line.Start.Row <= BottomLeft.Row;
        if (!isRowWithinRectangle)
        {
            return false;
        }

        (int left, int right) = (line.Start.Column, line.End.Column);
        if (left > right)
        {
            (left, right) = (right, left);
        }

        bool leftIsWithin = TopLeft.Column <= left && left <= TopRight.Column;
        bool rightIsWithin = TopLeft.Column <= right && right <= TopRight.Column;
        bool fullyIntersects = left < TopLeft.Column && right > TopRight.Column;

        return leftIsWithin || rightIsWithin || fullyIntersects;
    }

    private bool IntersectsVerticalLine(Line line)
    {
        bool isColumnWithinRectangle = TopLeft.Column <= line.Start.Column && line.Start.Column <= TopRight.Column;
        if (!isColumnWithinRectangle)
        {
            return false;
        }

        (int top, int bottom) = (line.Start.Row, line.End.Row);
        if (top > bottom)
        {
            (top, bottom) = (bottom, top);
        }

        bool topIsWithin = TopLeft.Row <= top && top <= BottomLeft.Row;
        bool bottomIsWithin = TopLeft.Row <= bottom && bottom <= BottomLeft.Row;
        bool fullyIntersects = top < TopLeft.Row && bottom > BottomLeft.Row;

        return topIsWithin || bottomIsWithin || fullyIntersects;
    }
}
