class Board
{
    public int Rows { get; }
    public int Columns { get; }
    public Ship[] Ships { get; }

    public Board(int rows, int columns, Ship[] ships)
    {
        if (rows <= 0 || columns <= 0)
            throw new ArgumentException($"Board size must be greater than 0: Rows={rows}, Columns={columns}");

        if (ships == null || ships.Length == 0)
            throw new ArgumentException("Board must have at least one ship");

        // Пункт 1.1 (из ДЗ 6) — проверка что корабль внутри поля, в конструкторе Board
        foreach (var ship in ships)
        {
            if (!ship.IsInsideBoard(rows, columns))
                throw new ArgumentException("Ship is outside the board boundaries");
        }

        Rows = rows;
        Columns = columns;
        Ships = ships;
    }

    public bool IsInside(Position position)
    {
        return position.X >= 0 && position.X < Rows &&
               position.Y >= 0 && position.Y < Columns;
    }

    public Ship? FindShip(Position position)
    {
        return Ships.FirstOrDefault(s => s.IsOnPosition(position));
    }

    // Пункт 4 — вывод доски, hideShips скрывает целые палубы (для доски компьютера)
    public void Print(IReadOnlyList<Shot> shots, bool hideShips)
    {
        for (int y = 0; y < Columns; y++)
        {
            for (int x = 0; x < Rows; x++)
            {
                var position = new Position(x, y);
                var shot = shots.FirstOrDefault(s => s.Board == this && s.Position.Equals(position));

                char symbol;
                if (shot != null)
                    symbol = shot.Ship != null ? 'X' : 'O';
                else if (!hideShips && FindShip(position) != null)
                    symbol = 'S';
                else
                    symbol = '.';

                Console.Write(symbol);
            }
            Console.WriteLine();
        }
    }
}