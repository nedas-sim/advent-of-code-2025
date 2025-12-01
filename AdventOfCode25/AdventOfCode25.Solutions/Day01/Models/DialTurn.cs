using System.Diagnostics;

namespace AdventOfCode25.Solutions.Day01.Models;

public class DialTurn
{
    private DialRotation _rotation;
    private int _turns;

    private const int MAX_VALUE = 100;

    public DialTurn(string inputLine)
    {
        _rotation = inputLine[0] switch
        {
            'L' => DialRotation.Left,
            'R' => DialRotation.Right,
            _ => throw new UnreachableException($"Could not rotation direction from input line '{inputLine}'"),
        };

        _turns = int.Parse(inputLine[1..]);
    }

    public int Turn(int currentPoint)
    {
        currentPoint += _rotation switch
        {
            DialRotation.Left => -_turns,
            DialRotation.Right => _turns,
            _ => throw new UnreachableException(),
        };

        return (currentPoint + MAX_VALUE) % MAX_VALUE;
    }

    public int Turn(int currentPoint, out int totalPassesThroughZero)
    {
        int fullRotations = _turns / MAX_VALUE;

        totalPassesThroughZero = fullRotations;
        _turns -= fullRotations * MAX_VALUE;

        totalPassesThroughZero += DoesOverflowOnSmallTurn(currentPoint)
            ? 1
            : 0;

        currentPoint += _rotation switch
        {
            DialRotation.Left => -_turns,
            DialRotation.Right => _turns,
            _ => throw new UnreachableException(),
        };

        return (currentPoint + MAX_VALUE) % MAX_VALUE;
    }

    private bool DoesOverflowOnSmallTurn(int currentValue)
    {
        return (_rotation, _turns, currentValue) switch
        {
            (_, _, 0) => false,
            (DialRotation.Left, _, _) when currentValue <= _turns => true,
            (DialRotation.Right, _, _) when currentValue + _turns >= MAX_VALUE => true,
            _ => false,
        };
    }
}

public enum DialRotation
{
    Left = 1,
    Right,
}