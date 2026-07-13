class Board
{
    public int Rows { get; }
    public int Columns { get; }
    public Ship Ship { get; }

    public Board(int rows, int columns, Ship ship)
    {
        Rows = rows;
        Columns = columns;
        Ship = ship;
    }

    public bool IsInside(Position position)
    {
        return position.X >= 0 && position.X < Rows &&
               position.Y >= 0 && position.Y < Columns;
    }

    public bool HasShip(Position position)
    {
        return position.Y == Ship.Position.Y &&
               position.X >= Ship.Position.X &&
               position.X < Ship.Position.X + Ship.Length;
    }
}