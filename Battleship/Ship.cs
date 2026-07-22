class Ship
{
    public Position Position { get; }
    public int Length { get; }

    public Ship(Position position, int length)
    {
        Position = position;
        Length = length;
    }
}