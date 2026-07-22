class Board
{
    public int Rows { get; }
    public int Columns { get; }
    public Ship Ship { get; }

    public Board(int rows, int columns, Ship ship)
    {
        // Пункт 1 — валидация размера доски
        if (rows <= 0 || columns <= 0)
            throw new ArgumentException($"Board size must be greater than 0: Rows={rows}, Columns={columns}");

        if (ship == null)
            throw new ArgumentException("Ship cannot be null");

        Rows = rows;
        Columns = columns;
        Ship = ship;

        // Пункт 1 — проверка что корабль находится внутри поля
        if (!IsShipInsideBoard())
            throw new ArgumentException("Ship is outside the board boundaries");
    }

    private bool IsShipInsideBoard()
    {
        var shipStart = new Position(Ship.Position.X, Ship.Position.Y);
        var shipEnd = new Position(Ship.Position.X + Ship.Length - 1, Ship.Position.Y);
        return IsInside(shipStart) && IsInside(shipEnd);
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

    // Пункт 4 — возвращает корабль если попали, null если промах
    public Ship? FindShip(Position position)
    {
        return HasShip(position) ? Ship : null;
    }
}