class Game
{
    private readonly GameSettings _settings;

    // Пункт 1.5 — общая история всех выстрелов игры
    private readonly List<Shot> _shots = new List<Shot>();

    public Game(GameSettings settings)
    {
        _settings = settings;
    }

    public void Play(Board userBoard)
    {
        var opponentBoard = GenerateOpponentBoard(userBoard.Ships.Length);

        // Пункт 1.3 — Game работает через IPlayer, без проверки конкретных типов
        IPlayer human = new HumanPlayer("Player", userBoard);
        IPlayer computer = new ComputerPlayer("Computer", opponentBoard);

        while (true)
        {
            try
            {
                var userShot = human.Shoot(opponentBoard, _shots);
                _shots.Add(userShot);
                Console.WriteLine(userShot.Result == ShootResult.Hit ? "Hit!" : "Miss!");

                var computerShot = computer.Shoot(userBoard, _shots);
                _shots.Add(computerShot);
                Console.WriteLine(computerShot.Result == ShootResult.Hit ? "Computer hit your ship!" : "Computer missed!");

                Console.WriteLine("\nYour board:");
                userBoard.Print(_shots, hideShips: false);
                Console.WriteLine("\nOpponent board:");
                opponentBoard.Print(_shots, hideShips: true);

                // Пункт 5 — LINQ подсчёт потопленных кораблей
                int userSunk = userBoard.Ships.Count(s => s.IsSunk);
                int opponentSunk = opponentBoard.Ships.Count(s => s.IsSunk);
                Console.WriteLine($"\nSunk ships — Yours: {userSunk}/{userBoard.Ships.Length} | Opponent: {opponentSunk}/{opponentBoard.Ships.Length}");

                if (opponentSunk == opponentBoard.Ships.Length)
                {
                    Console.WriteLine("\nYou sank all opponent ships! You win!");
                    return;
                }

                if (userSunk == userBoard.Ships.Length)
                {
                    Console.WriteLine("\nComputer sank all your ships! Computer wins!");
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }

    // Пункт 3 — генерация доски компьютера с тем же количеством кораблей что у игрока
    private Board GenerateOpponentBoard(int shipCount)
    {
        var random = new Random();
        var ships = new List<Ship>();

        while (ships.Count < shipCount)
        {
            var candidate = random.NextShip(_settings);

            // Пункт 3 — проверяем что новый корабль не пересекается с уже созданными
            bool intersects = ships.Any(s => s.Intersects(candidate));
            if (!intersects)
                ships.Add(candidate);
        }

        return new Board(_settings.Rows, _settings.Columns, ships.ToArray());
    }
}