namespace AdventOfCode25.Solutions.Day10.Models;

public class Machine
{
    private readonly LightDiagram _desiredDiagram;
    private readonly List<WiringSchematic> _schematics;

    private readonly Dictionary<int, bool> _cache = [];

    public int Result { get; private set; }

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

        List<LightDiagram> currentLevel = [initial];
        List<LightDiagram> nextLevel = [];

        for (int level = 1; ; level++)
        {
            foreach (LightDiagram lightDiagramAtCurrentLevel in currentLevel)
            {
                if (SolveInternal(lightDiagramAtCurrentLevel, out List<LightDiagram> forNextLevel))
                {
                    Result = level;
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

        if (_cache.TryGetValue(current.Id, out bool cached))
        {
            return cached;
        }

        foreach (WiringSchematic schematic in _schematics)
        {
            LightDiagram toggled = current.ApplyToggle(schematic);
            diagramsForNextLevel.Add(toggled);

            if (toggled.IsEqual(_desiredDiagram))
            {
                _cache[current.Id] = true;
                return true;
            }
        }

        _cache[current.Id] = false;
        return false;
    }
}
