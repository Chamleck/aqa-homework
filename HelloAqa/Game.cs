class Game
{
    private readonly Player _human;
    private readonly Player _computer;
    private readonly int _totalRounds;

    // Пункт 1 — конструктор принимает игроков и количество раундов
    public Game(Player human, Player computer, int totalRounds)
    {
        _human = human;
        _computer = computer;
        _totalRounds = totalRounds;
    }

    // Пункт 7 — Play() использует _totalRounds из конструктора
    public void Play()
    {
        _human.ResetScore();
        _computer.ResetScore();

        int roundsPlayed = 0;

        while (roundsPlayed < _totalRounds)
        {
            Console.Clear();
            Console.WriteLine($"Round {roundsPlayed + 1} of {_totalRounds}");
            Console.WriteLine($"Score — {_human.Name}: {_human.Score} | {_computer.Name}: {_computer.Score}");
            Console.WriteLine();
            Console.WriteLine("1 - Rock");
            Console.WriteLine("2 - Paper");
            Console.WriteLine("3 - Scissors");
            Console.WriteLine("4 - Well");

            // Получаем ход игрока
            var playerMove = new Move();
            playerMove.ReadFromConsole();

            // Пункт 3 — если ход невалидный, ничего не делаем
            if (!playerMove.IsValid())
            {
                Console.WriteLine("Invalid input. Round not counted. Press any key...");
                Console.ReadKey();
                continue;
            }

            // Генерируем ход компьютера только если ход игрока валидный
            var computerMove = new Move();
            computerMove.GenerateRandom();

            // Пункт 5 — отдельный метод определяет результат и начисляет очко
            var result = DetermineResult(playerMove, computerMove);

            roundsPlayed++;

            // Пункт 8 — выводим номер раунда, ходы и результат
            result.Print(roundsPlayed);
            Console.WriteLine($"Score — {_human.Name}: {_human.Score} | {_computer.Name}: {_computer.Score}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        ShowFinalResult();
    }

    // Пункт 5 — отдельный метод принимает ходы, определяет результат,
    // начисляет очко победителю и возвращает GameResult
    private GameResult DetermineResult(Move playerMove, Move computerMove)
    {
        if (playerMove.Number == computerMove.Number)
            return new GameResult(playerMove, computerMove, "Draw!");

        bool playerWins =
            (playerMove.Number == 1 && computerMove.Number == 3) ||
            (playerMove.Number == 2 && computerMove.Number == 1) ||
            (playerMove.Number == 3 && computerMove.Number == 2) ||
            (playerMove.Number == 4 && computerMove.Number == 1) ||
            (playerMove.Number == 4 && computerMove.Number == 3) ||
            (playerMove.Number == 2 && computerMove.Number == 4);

        if (playerWins)
        {
            _human.AddPoint();
            return new GameResult(playerMove, computerMove, "You win this round!");
        }

        _computer.AddPoint();
        return new GameResult(playerMove, computerMove, "Computer wins this round!");
    }

    private void ShowFinalResult()
    {
        Console.Clear();
        Console.WriteLine("=== GAME OVER ===");
        Console.WriteLine($"Final Score — {_human.Name}: {_human.Score} | {_computer.Name}: {_computer.Score}");

        if (_human.Score > _computer.Score)
            Console.WriteLine($"{_human.Name} won the game!");
        else if (_computer.Score > _human.Score)
            Console.WriteLine($"{_computer.Name} won the game!");
        else
            Console.WriteLine("It's a draw!");
    }
}