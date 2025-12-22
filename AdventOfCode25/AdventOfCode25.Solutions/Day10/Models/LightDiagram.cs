namespace AdventOfCode25.Solutions.Day10.Models;

public record LightDiagram
{
    public required List<bool> Indicators { get; set; }

    public int Id => HashCode.Combine(Indicators.Select(x => x.GetHashCode()));

    public LightDiagram ApplyToggle(WiringSchematic schematic)
    {
        return new LightDiagram
        {
            Indicators = Indicators
                .Index()
                .Select(x => schematic.ToggleIndices.Contains(x.Index)
                    ? !x.Item
                    : x.Item
                ).ToList(),
        };
    }

    public bool IsEqual(LightDiagram other)
    {
        return Indicators.SequenceEqual(other.Indicators);
    }
}

public class WiringSchematic
{
    public readonly HashSet<decimal> ToggleIndices = [];
}