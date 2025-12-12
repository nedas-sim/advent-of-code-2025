namespace AdventOfCode25.Solutions.Day10.Models;

public record LightDiagram
{
    public required List<bool> Indicators { get; set; }

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
    public readonly HashSet<int> ToggleIndices = [];
}

public class Machine
{
    private readonly LightDiagram _desiredDiagram;
    private readonly List<WiringSchematic> _schematics;

    public Machine(string inputLine)
    {
        _desiredDiagram = new()
        {
            Indicators = inputLine[1..inputLine.IndexOf(']')]
                .Select(x => x == '#')
                .ToList(),
        };

        _schematics = [];

        ReadOnlySpan<char> remainingString = inputLine.AsSpan()[inputLine.IndexOf(']')..];

        while (true)
        {
            int leftParenIndex = remainingString.IndexOf('(');
            int rightParenIndex = remainingString.IndexOf(')');

            if (leftParenIndex < 0 || rightParenIndex < 0)
            {
                break;
            }

            ReadOnlySpan<char> toggleIndices = remainingString[(leftParenIndex + 1)..rightParenIndex];
            WiringSchematic schematic = new();

            foreach (Range singleIndexRange in toggleIndices.Split(','))
            {
                schematic.ToggleIndices.Add(int.Parse(toggleIndices[singleIndexRange]));
            }

            _schematics.Add(schematic);

            remainingString = remainingString[(rightParenIndex + 1)..];
        }
    }

    public int Solve()
    {
        LightDiagram initial = new()
        {
            Indicators = _desiredDiagram.Indicators.Select(x => false).ToList(),
        };

        List<LightDiagram> currentLevel = [ initial ];
        List<LightDiagram> nextLevel = [];

        for (int level = 1; ; level++)
        {
            foreach (LightDiagram lightDiagramAtCurrentLevel in currentLevel)
            {
                if (SolveInternal(lightDiagramAtCurrentLevel, out List<LightDiagram> forNextLevel))
                {
                    return level;
                }

                nextLevel.AddRange(forNextLevel);
            }

            currentLevel = [.. nextLevel];
        }
    }

    private bool SolveInternal(LightDiagram current, out List<LightDiagram> diagramsForNextLevel)
    {
        diagramsForNextLevel = [];

        foreach (WiringSchematic schematic in _schematics)
        {
            LightDiagram toggled = current.ApplyToggle(schematic);
            diagramsForNextLevel.Add(toggled);

            if (toggled.IsEqual(_desiredDiagram))
            {
                return true;
            }
        }

        return false;
    }
}