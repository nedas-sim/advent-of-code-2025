namespace AdventOfCode25.Solutions.Day02.Models;

public class ProductIdRange
{
    private long _firstId;
    private long _lastId;

    public ProductIdRange(string inputLine)
    {
        string[] splitLine = inputLine.Split('-');

        _firstId = long.Parse(splitLine[0]);
        _lastId = long.Parse(splitLine[1]);
    }

    public IEnumerable<long> GetInvalidIds(int? requiredEqualSplits = null)
    {
        for (long productIdToCheck = _firstId; productIdToCheck <= _lastId; productIdToCheck++)
        {
            string idAsString = $"{productIdToCheck}";

            if (requiredEqualSplits.HasValue && idAsString.Length % requiredEqualSplits != 0)
            {
                continue;
            }

            if (requiredEqualSplits.HasValue)
            {
                if (IsInvalidId(idAsString, idAsString.Length / requiredEqualSplits.Value))
                {
                    yield return productIdToCheck;
                }

                continue;
            }

            bool anySubstringRepeats = Enumerable.Range(1, idAsString.Length / 2)
                .Select(x => IsInvalidId(idAsString, x))
                .Any(x => x);

            if (anySubstringRepeats)
            {
                yield return productIdToCheck;
            }
        }
    }

    public static bool IsInvalidId(string idAsString, int suffixLength)
    {
        if (idAsString.Length % suffixLength != 0)
        {
            return false;
        }

        return IsRepeating(idAsString, idAsString[..suffixLength]);
    }

    public static bool IsRepeating(string valueToCheck, string suffix)
    {
        if (valueToCheck.Length < suffix.Length)
        {
            return string.IsNullOrEmpty(valueToCheck);
        }

        return valueToCheck.StartsWith(suffix) && IsRepeating(valueToCheck[suffix.Length..], suffix);
    }
}
