class ComputerPlayer : IPlayer, IShooter
{
    public string Name { get; }
    public Board Board { get; }
    private readonly Random _random = new Random();

    public ComputerPlayer(string name, Board board)
    {
        Name = name;
        Board = board;
    }

    public Shot Shoot(Board targetBoard, IReadOnlyList<Shot> shotHistory)
    {
        Position position;

        // Компьютер сам подбирает свободную клетку, без исключений
        do
        {
            int x = _random.Next(0, targetBoard.Rows);
            int y = _random.Next(0, targetBoard.Columns);
            position = new Position(x, y);
        }
        while (shotHistory.Any(s => s.Board == targetBoard && s.Position.Equals(position)));

        var ship = targetBoard.FindShip(position);
        var shot = new Shot(targetBoard, position, ship);

        ship?.RegisterHit(shot);

        Console.WriteLine($"{Name} shoots at X:{position.X}, Y:{position.Y}");

        return shot;
    }
}