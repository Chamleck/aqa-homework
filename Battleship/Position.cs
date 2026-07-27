readonly struct Position
{
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y)
    {
        // Пункт 1.4 — readonly struct вместо class, значения нельзя менять после создания
        if (x < 0 || y < 0)
            throw new ArgumentException($"Position coordinates cannot be negative: X={x}, Y={y}");

        X = x;
        Y = y;
    }

    // Equals и GetHashCode нужны чтобы сравнивать позиции через == и в LINQ (Any, Where и т.д.)
    public override bool Equals(object? obj)
    {
        return obj is Position other && X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}