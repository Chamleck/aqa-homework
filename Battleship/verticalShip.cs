class VerticalShip : Ship
{
    public VerticalShip(Position position, int length) : base(position, length)
    {
    }

    public override bool IsOnPosition(Position position)
    {
        return position.X == Position.X &&
               position.Y >= Position.Y &&
               position.Y < Position.Y + Length;
    }

    public override bool IsInsideBoard(int rows, int columns)
    {
        return Position.X >= 0 && Position.Y >= 0 &&
               Position.X < rows &&
               Position.Y + Length <= columns;
    }

    public override bool Intersects(Ship other)
    {
        for (int y = Position.Y; y < Position.Y + Length; y++)
        {
            if (other.IsOnPosition(new Position(Position.X, y)))
                return true;
        }
        return false;
    }
}