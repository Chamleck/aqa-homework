class HorizontalShip : Ship
{
    public HorizontalShip(Position position, int length) : base(position, length)
    {
    }

    public override bool IsOnPosition(Position position)
    {
        return position.Y == Position.Y &&
               position.X >= Position.X &&
               position.X < Position.X + Length;
    }

    public override bool IsInsideBoard(int rows, int columns)
    {
        return Position.X >= 0 && Position.Y >= 0 &&
               Position.X + Length <= rows &&
               Position.Y < columns;
    }

    // Перебираем все клетки этого корабля, проверяем занимает ли их other
    public override bool Intersects(Ship other)
    {
        for (int x = Position.X; x < Position.X + Length; x++)
        {
            if (other.IsOnPosition(new Position(x, Position.Y)))
                return true;
        }
        return false;
    }
}