using AdventOfCode25.Solutions.Shared.Models;

namespace AdventOfCode25.Solutions.Day09.Models;

public record HorizontalLine(Coordinates Left, Coordinates Right)
{
    public bool ContainsColumn(int column)
    {
        return Left.Column <= column && column <= Right.Column;
    }
}

public record VerticalLine(Coordinates Top, Coordinates Bottom)
{
    public bool ContainsRow(int row)
    {
        return Top.Row <= row && row <= Bottom.Row;
    }
}

public class LineCollection
{
    private readonly List<HorizontalLine> _horizontalLines = [];
    private readonly List<VerticalLine> _verticalLines = [];

    private readonly HashSet<Coordinates> _coordinates = [];

    public int Left => _horizontalLines.Select(x => x.Left.Column).Min();
    public int Right => _horizontalLines.Select(x => x.Right.Column).Max();
    public int Top => _verticalLines.Select(x => x.Top.Row).Min();
    public int Bottom => _verticalLines.Select(x => x.Bottom.Row).Max();

    public int ColumnsOnLeft => _verticalLines.Where(x => x.Top.Column == Left).Count();
    public int ColumnsOnRight => _verticalLines.Where(x => x.Top.Column == Right).Count();
    public int RowsOnTop => _horizontalLines.Where(x => x.Left.Row == Top).Count();
    public int RowsOnBottom => _horizontalLines.Where(x => x.Left.Row == Bottom).Count();

    public bool IsCorner(Coordinates coords)
    {
        return _coordinates.Contains(coords);
    }

    public void AddLine(Coordinates one, Coordinates two)
    {
        _coordinates.Add(one);
        _coordinates.Add(two);

        if (one.Row == two.Row)
        {
            (Coordinates left, Coordinates right) = one.Column < two.Column
                ? (one, two)
                : (two, one);

            _horizontalLines.Add(new(left, right));
            return;
        }

        (Coordinates top, Coordinates bottom) = one.Row < two.Row
            ? (one, two)
            : (two, one);

        _verticalLines.Add(new(top, bottom));
    }

    public int HorizontalLinesOnColumn(int column, Func<int, bool> rowPredicate)
    {
        return _horizontalLines
            .Where(x => x.ContainsColumn(column))
            .Where(x => rowPredicate(x.Left.Row))
            .Count();
    }

    public int VerticalLinesOnColumn(int column, Func<int, bool> rowPredicate)
    {
        return _verticalLines
            .Where(x => x.Top.Column == column)
            .Where(x => rowPredicate(x.Top.Row) && rowPredicate(x.Bottom.Row))
            .Count();
    }

    public int HorizontalLinesOnRow(int row, Func<int, bool> columnPredicate)
    {
        return _horizontalLines
            .Where(x => x.Left.Row == row)
            .Where(x => columnPredicate(x.Left.Column) && columnPredicate(x.Right.Column))
            .Count();
    }

    public int VerticalLinesOnRow(int row, Func<int, bool> columnPredicate)
    {
        return _verticalLines
            .Where(x => x.ContainsRow(row))
            .Where(x => columnPredicate(x.Top.Column))
            .Count();
    }
}

public class Rectangle
{
    private readonly LineCollection _lineCollection;

    public Coordinates TopLeft { get; private set; }
    public Coordinates TopRight { get; private set; }
    public Coordinates BottomLeft { get; private set; }
    public Coordinates BottomRight { get; private set; }

    public Rectangle(
        Coordinates one,
        Coordinates two,
        LineCollection lineCollection)
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

        _lineCollection = lineCollection;
    }


    /*public Coordinates CornerOne => cornerOne;
    public Coordinates CornerTwo => cornerTwo;
    public Coordinates CornerThree => new(cornerOne.Row, cornerTwo.Column);
    public Coordinates CornerFour => new(cornerTwo.Row, cornerOne.Column);*/

    public bool IsOutOfBounds()
    {
        return IsOutOfBoundsRight(TopLeft) != IsOutOfBoundsRight(TopRight)
            || IsOutOfBoundsRight(BottomLeft) != IsOutOfBoundsRight(BottomRight)

            || IsOutOfBoundsDown(TopLeft) != IsOutOfBoundsDown(BottomLeft)
            || IsOutOfBoundsDown(TopRight) != IsOutOfBoundsDown(BottomRight);

        /*return IsPointOfOutBounds(CornerOne)
            || IsPointOfOutBounds(CornerTwo)
            || IsPointOfOutBounds(CornerThree)
            || IsPointOfOutBounds(CornerFour);*/
    }

    /*private bool IsPointOfOutBounds(Coordinates point)
    {
        if (lineCollection.IsCorner(point))
        {
            return false;
        }

        return IsOutOfBoundsUp(point)
            || IsOutOfBoundsDown(point)
            || IsOutOfBoundsLeft(point)
            || IsOutOfBoundsRight(point);
    }*/

    /*private bool IsOutOfBoundsUp(Coordinates point)
    {
        int horizontalLinesAbove =
            lineCollection.HorizontalLinesOnColumn(point.Column, row => row <= point.Row);

        int verticalLinesAbove =
            lineCollection.VerticalLinesOnColumn(point.Column, row => row <= point.Row);

        *//*int boundaryAdjustment = 
            point.Column == lineCollection.Left || point.Column == lineCollection.Right
                ? 1
                : 0;*//*

        int boundaryAdjustment =
            point.Column == lineCollection.Left ? lineCollection.ColumnsOnLeft
            : point.Column == lineCollection.Right ? lineCollection.ColumnsOnRight
            : 0;

        return (horizontalLinesAbove + verticalLinesAbove + boundaryAdjustment) % 2 == 0;
    }*/

    private int IsOutOfBoundsDown(Coordinates point)
    {
        int horizontalLinesBelow =
            _lineCollection.HorizontalLinesOnColumn(point.Column, row => row >= point.Row);

        int verticalLinesBelow =
            _lineCollection.VerticalLinesOnColumn(point.Column, row => row >= point.Row);

        int boundaryAdjustment =
            point.Column == _lineCollection.Left || point.Column == _lineCollection.Right
                ? 1
                : 0;

        /*int boundaryAdjustment =
            point.Column == _lineCollection.Left ? _lineCollection.ColumnsOnLeft
            : point.Column == _lineCollection.Right ? _lineCollection.ColumnsOnRight
            : 0;*/


        return horizontalLinesBelow + verticalLinesBelow + boundaryAdjustment;
    }

    /*private bool IsOutOfBoundsLeft(Coordinates point)
    {
        int horizontalLinesToTheLeft =
            lineCollection.HorizontalLinesOnRow(point.Row, column => column <= point.Column);

        int verticalLinesToTheLeft =
            lineCollection.VerticalLinesOnRow(point.Row, column => column <= point.Column);

        *//*int boundaryAdjustment =
            point.Row == lineCollection.Top || point.Row == lineCollection.Bottom
                ? 1
                : 0;*//*

        int boundaryAdjustment =
            point.Row == lineCollection.Top ? lineCollection.RowsOnTop
            : point.Row == lineCollection.Bottom ? lineCollection.RowsOnBottom
            : 0;

        return (horizontalLinesToTheLeft + verticalLinesToTheLeft + boundaryAdjustment) % 2 == 0;
    }*/

    private int IsOutOfBoundsRight(Coordinates point)
    {
        int horizontalLinesToTheRight =
            _lineCollection.HorizontalLinesOnRow(point.Row, column => column >= point.Column);

        int verticalLinesToTheRight =
            _lineCollection.VerticalLinesOnRow(point.Row, column => column >= point.Column);

        int boundaryAdjustment =
            point.Row == _lineCollection.Top || point.Row == _lineCollection.Bottom
                ? 1
                : 0;

        /*int boundaryAdjustment =
            point.Row == _lineCollection.Top ? _lineCollection.RowsOnTop
            : point.Row == _lineCollection.Bottom ? _lineCollection.RowsOnBottom
            : 0;*/

        return horizontalLinesToTheRight + verticalLinesToTheRight + boundaryAdjustment;
    }
}
