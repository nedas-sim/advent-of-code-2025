namespace AdventOfCode25.Solutions.Day01.Models;

public interface IDialTurn<TSelf>
where TSelf : class, IDialTurn<TSelf>
{
    int Turn(int currentPoint);
    int Turn(int currentPoint, out int totalPassesThroughZero);

    static abstract TSelf BuildNext(string inputLine, TSelf? current = null);
}
