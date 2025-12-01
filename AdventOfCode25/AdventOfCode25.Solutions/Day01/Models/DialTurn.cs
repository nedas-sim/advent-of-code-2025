using System.Diagnostics;

namespace AdventOfCode25.Solutions.Day01.Models;

public class DialTurn
{
    private DialRotation _rotation;
    private int _turns;

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

        return (currentPoint + 100) % 100;
    }
}

public enum DialRotation
{
    Left = 1,
    Right,
}