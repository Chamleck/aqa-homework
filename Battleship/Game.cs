class Game
{
    public int PlayerHits { get; private set; }
    public int ComputerHits { get; private set; }

    private List<Shot> Shots { get; } = new List<Shot>();

    public void Play(Board userBoard)
    {
        Board opponentBoard;

        try
        {
            opponentBoard = GenerateOpponentBoard(userBoard.Rows, userBoard.Columns);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to generate opponent board: {e.Message}");
            return;
        }

        var roundCount = 0;

        while (true)
        {
            roundCount++;

            try
            {
                if (!TryReadFromConsole("X", roundCount, out var xPosition))
                    continue;
                if (!TryReadFromConsole("Y", roundCount, out var yPosition))
                    continue;

                var userShotPosition = new Position(xPosition, yPosition);

                if (!opponentBoard.IsInside(userShotPosition))
                    throw new Exception("Shot position is outside the board!");

                bool alreadyShot = Shots.Any(s =>
                    s.Board == opponentBoard &&
                    s.Position.X == userShotPosition.X &&
                    s.Position.Y == userShotPosition.Y);

                if (alreadyShot)
                    throw new Exception($"You already shot at X:{xPosition}, Y:{yPosition}!");

                var userShot = MakeShot(opponentBoard, userShotPosition);

                if (userShot.IsHit)
                {
                    Console.WriteLine("Hit!");
                    PlayerHits++;
                }
                else
                {
                    Console.WriteLine("Miss!");
                }

                var random = new Random();
                Position computerShotPosition;

                do
                {
                    int computerX = random.Next(0, userBoard.Rows);
                    int computerY = random.Next(0, userBoard.Columns);
                    computerShotPosition = new Position(computerX, computerY);
                }
                while (Shots.Any(s =>
                    s.Board == userBoard &&
                    s.Position.X == computerShotPosition.X &&
                    s.Position.Y == computerShotPosition.Y));

                var computerShot = MakeShot(userBoard, computerShotPosition);

                Console.WriteLine($"Computer shoots at X:{computerShotPosition.X}, Y:{computerShotPosition.Y}");

                if (computerShot.IsHit)
                {
                    Console.WriteLine("Computer hit your ship!");
                    ComputerHits++;
                }
                else
                {
                    Console.WriteLine("Computer missed!");
                }

                PrintStats(userBoard, opponentBoard);
            }
            catch (Exception e)
            {
                roundCount--; // ← откатываем счётчик — раунд не засчитан
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }

    private Shot MakeShot(Board board, Position position)
    {
        var ship = board.FindShip(position);
        var shot = new Shot(board, position, ship);
        Shots.Add(shot);
        return shot;
    }

    private void PrintStats(Board userBoard, Board opponentBoard)
    {
        Console.WriteLine("\n=== STATISTICS ===");
        PrintBoardStats("Your board", userBoard);
        PrintBoardStats("Opponent board", opponentBoard);
        Console.WriteLine($"Total score — You: {PlayerHits} | Computer: {ComputerHits}");
        Console.WriteLine("==================\n");
    }

    private void PrintBoardStats(string boardName, Board board)
    {
        var boardShots = Shots.Where(s => s.Board == board).ToList();
        int totalShots = boardShots.Count();
        int hits = boardShots.Count(s => s.IsHit);
        int misses = boardShots.Count(s => !s.IsHit);
        bool anyMiss = boardShots.Any(s => !s.IsHit);
        var firstHit = boardShots.FirstOrDefault(s => s.IsHit);
        var hitPositions = boardShots
            .Where(s => s.IsHit)
            .Select(s => $"X:{s.Position.X}, Y:{s.Position.Y}")
            .ToList();

        Console.WriteLine($"\n--- {boardName} ---");
        Console.WriteLine($"Total shots: {totalShots}");
        Console.WriteLine($"Hits: {hits}");
        Console.WriteLine($"Misses: {misses}");
        Console.WriteLine($"Any miss: {anyMiss}");

        if (firstHit != null)
            Console.WriteLine($"First hit: X:{firstHit.Position.X}, Y:{firstHit.Position.Y}");
        else
            Console.WriteLine("First hit: none yet");

        Console.WriteLine($"All hit positions: {(hitPositions.Any() ? string.Join(", ", hitPositions) : "none")}");
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