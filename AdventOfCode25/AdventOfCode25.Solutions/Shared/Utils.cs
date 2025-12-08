using System.Numerics;

namespace AdventOfCode25.Solutions.Shared;

public static class Utils
{
    public static T[] SplitByDash<T>(string input)
        where T : IParsable<T>
    {
        string[] splitInput = input.Split('-');
        return [ ..splitInput.Select(x => T.Parse(x, null)) ];
    }

    public static IEnumerable<T> SplitBySpaces<T>(string input)
        where T : IParsable<T>
    {
        string[] splitInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return splitInput.Select(x => T.Parse(x, null));
    }

    public static IEnumerable<T> SplitByComma<T>(string input)
        where T : IParsable<T>
    {
        string[] splitInput = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return splitInput.Select(x => T.Parse(x, null));
    }

    extension<T>(IEnumerable<T> values) where T : IMultiplyOperators<T, T, T>, IMultiplicativeIdentity<T, T>
    {
        public T Product()
        {
            return values.Aggregate(T.MultiplicativeIdentity, func: (a, b) => a * b);
        }
    }
}
