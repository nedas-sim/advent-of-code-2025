namespace AdventOfCode25.Solutions.Shared;

public static class Utils
{
    public static T[] SplitByDash<T>(string input)
        where T : IParsable<T>
    {
        string[] splitInput = input.Split('-');
        return [ ..splitInput.Select(x => T.Parse(x, null)) ];
    }
}
