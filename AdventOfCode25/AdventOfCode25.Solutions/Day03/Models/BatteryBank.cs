namespace AdventOfCode25.Solutions.Day03.Models;

public class BatteryBank(string inputLine)
{
    public int FindHighestJoltage()
    {
        int highestJoltage = GetViableCombinations(inputLine)
            .SelectMany(first =>
            {
                IEnumerable<(int Number, string Tail)> viableCombinations = GetViableCombinations(first.Tail);
                return viableCombinations.Select(next => (Number: first.Number * 10 + next.Number, next.Tail));
            })
            .Where(x => x.Number >= 10)
            .Select(x => x.Number)
            .First();

        return highestJoltage;
    }

    private static IEnumerable<(int Number, string Tail)> GetViableCombinations(string tail)
    {
        return FromNineToZero()
            .Select(x => (Number: x, Tail: GetStringTail(tail, $"{x}")))
            .Where(x => x.Tail is not null)
            .OfType<(int Number, string Tail)>();
    }

    public static string? GetStringTail(string input, string toFind)
    {
        int index = input.IndexOf(toFind);
        
        if (index == -1)
        {
            return null;
        }

        if (index == input.Length - 1)
        {
            return string.Empty;
        }

        return input[(index + 1)..];
    }

    private static IEnumerable<int> FromNineToZero()
    {
        yield return 9;
        yield return 8;
        yield return 7;
        yield return 6;
        yield return 5;
        yield return 4;
        yield return 3;
        yield return 2;
        yield return 1;
        yield return 0;
    }
}
