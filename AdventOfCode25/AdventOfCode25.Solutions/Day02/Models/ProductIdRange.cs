namespace AdventOfCode25.Solutions.Day02.Models;

public class ProductIdRange
{
    private long _firstId;
    private long _lastId;

    /*private string _firstId;
    private string _lastId;*/

    public ProductIdRange(string inputLine)
    {
        string[] splitLine = inputLine.Split('-');

        /*_firstId = splitLine[0];
        _lastId = splitLine[1];*/

        _firstId = long.Parse(splitLine[0]);
        _lastId = long.Parse(splitLine[1]);
    }

    public IEnumerable<long> GetInvalidIds()
    {
        for (long productIdToCheck = _firstId; productIdToCheck <= _lastId; productIdToCheck++)
        {
            if (IsInvalidId(productIdToCheck))
            {
                yield return productIdToCheck;
            }
        }
    }

    private static bool IsInvalidId(long id)
    {
        string idAsString = $"{id}";

        if (idAsString.Length % 2 != 0)
        {
            return false;
        }

        return idAsString[..(idAsString.Length / 2)] == idAsString.Substring(idAsString.Length / 2);
    }
}
