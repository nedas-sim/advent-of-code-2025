namespace AdventOfCode25.Solutions.Day08.Models;

public record struct ThreeDimensionCoordinate(int X, int Y, int Z)
{
    public readonly long DistanceSquared(ThreeDimensionCoordinate other)
        => DiffSquared(X, other.X) + DiffSquared(Y, other.Y) + DiffSquared(Z, other.Z);

    private static long DiffSquared(long a, long b) => (a - b) * (a - b); 
}
