namespace AdventOfCode25.Solutions.Day08.Models;

public class Circuit(int initialIndex)
{
    private readonly HashSet<int> _junctionBoxIndices = [initialIndex];

    public bool HasIndex(int index) => _junctionBoxIndices.Contains(index);

    public void CombineCircuit(Circuit circuit)
    {
        foreach (int index in circuit._junctionBoxIndices)
        {
            _junctionBoxIndices.Add(index);
        }
    }

    public long IndexCount => _junctionBoxIndices.Count;
}
