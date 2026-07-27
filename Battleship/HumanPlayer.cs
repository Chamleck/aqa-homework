class HumanPlayer : IPlayer, IShooter
{
    public string Name { get; }
    public Board Board { get; }

    public HumanPlayer(string name, Board board)
    {
        Name = name;
        Board = board;
    }

    public Shot Shoot(Board targetBoard, IReadOnlyList<Shot> shotHistory)
    {
        Console.WriteLine($"{Name}, enter your X coordinate:");
        if (!int.TryParse(Console.ReadLine(), out int x))
            throw new Exception("Invalid input for X coordinate");

        Console.WriteLine($"{Name}, enter your Y coordinate:");
        if (!int.TryParse(Console.ReadLine(), out int y))
            throw new Exception("Invalid input for Y coordinate");

        var position = new Position(x, y);

        if (!targetBoard.IsInside(position))
            throw new Exception("Shot position is outside the board!");

        // Пункт 1.5 — проверка повторного выстрела с учётом доски и позиции
        bool alreadyShot = shotHistory.Any(s => s.Board == targetBoard && s.Position.Equals(position));
        if (alreadyShot)
            throw new Exception($"You already shot at X:{x}, Y:{y}!");

        var ship = targetBoard.FindShip(position);
        var shot = new Shot(targetBoard, position, ship);

        ship?.RegisterHit(shot);

        return shot;
    }
}