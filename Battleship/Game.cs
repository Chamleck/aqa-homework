class Game
{
    public int PlayerHits { get; private set; }
    public int ComputerHits { get; private set; }

    public void Play(Board userBoard)
    {
        var opponentBoard = GenerateOpponentBoard(userBoard.Rows, userBoard.Columns);
        var roundCount = 0;

        while (true)
        {
            roundCount++;

            if (!TryReadFromConsole("X", roundCount, out var xPosition))
                continue;
            if (!TryReadFromConsole("Y", roundCount, out var yPosition))
                continue;

            var userShot = new Position(xPosition, yPosition);

            if (!opponentBoard.IsInside(userShot))
            {
                Console.WriteLine("Invalid shot position!");
                continue;
            }

            if (opponentBoard.HasShip(userShot))
            {
                Console.WriteLine("Hit!");
                PlayerHits++;
            }
            else
            {
                Console.WriteLine("Miss!");
            }

            var random = new Random();
            int computerX = random.Next(0, userBoard.Rows);
            int computerY = random.Next(0, userBoard.Columns);
            var computerShot = new Position(computerX, computerY);

            Console.WriteLine($"Computer shoots at X:{computerX}, Y:{computerY}");

            if (userBoard.HasShip(computerShot))
            {
                Console.WriteLine("Computer hit your ship!");
                ComputerHits++;
            }
            else
            {
                Console.WriteLine("Computer missed!");
            }

            Console.WriteLine($"Score — You: {PlayerHits} | Computer: {ComputerHits}");
        }
    }

    private Board GenerateOpponentBoard(int rows, int columns)
    {
        var random = new Random();
        int shipLength = random.Next(1, Math.Min(rows, columns) + 1);
        int shipX = random.Next(0, rows - shipLength + 1);
        int shipY = random.Next(0, columns);

        var shipPosition = new Position(shipX, shipY);
        var ship = new Ship(shipPosition, shipLength);
        return new Board(rows, columns, ship);
    }

    private bool TryReadFromConsole(string coordinateName, int roundCount, out int coordinate)
    {
        Console.WriteLine($"Input your {coordinateName} coordinate for round {roundCount}:");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out coordinate))
        {
            Console.WriteLine("Invalid input");
            return false;
        }
        return true;
    }
}