// Entry point — создаём игру и запускаем
var game = new Game();
game.Play();

// Represents a player with name and score
class Player
{
    public string Name { get; set; }
    public int Score { get; private set; }

    public Player(string name)
    {
        Name = name;
        Score = 0;
    }

    // Increment player score by 1
    public void AddPoint()
    {
        Score++;
    }

    // Reset player score to 0
    public void ResetScore()
    {
        Score = 0;
    }
}

// Represents a single move in the game
class Move
{
    public int Number { get; private set; }
    public string Name { get; private set; }

    // Map of valid moves: number -> name
    private static readonly Dictionary<int, string> _moves = new()
    {
        { 1, "Rock" },
        { 2, "Paper" },
        { 3, "Scissors" },
        { 4, "Well" }
    };

    // Read and validate move from console input
    public void ReadFromConsole()
    {
        var input = Console.ReadLine();
        if (int.TryParse(input, out int number))
        {
            Number = number;
            Name = _moves.ContainsKey(number) ? _moves[number] : "Unknown";
        }
    }

    // Generate a random move for the computer (1-4)
    public void GenerateRandom()
    {
        var random = new Random();
        Number = random.Next(1, 5);
        Name = _moves[Number];
    }

    // Check if move number is valid (1 to 3 as per task, Well is bonus)
    public bool IsValid()
    {
        return Number >= 1 && Number <= 4;
    }
}

// Represents the result of a single round
class GameResult
{
    public Move PlayerMove { get; private set; }
    public Move ComputerMove { get; private set; }
    public string ResultText { get; private set; }

    public GameResult(Move playerMove, Move computerMove, string resultText)
    {
        PlayerMove = playerMove;
        ComputerMove = computerMove;
        ResultText = resultText;
    }

    // Print round result to console
    public void Print()
    {
        Console.WriteLine($"You chose: {PlayerMove.Name}");
        Console.WriteLine($"Computer chose: {ComputerMove.Name}");
        Console.WriteLine(ResultText);
    }
}

// Main game class — orchestrates players, moves and rounds
class Game
{
    private readonly Player _human;
    private readonly Player _computer;
    private int _totalRounds;

    public Game()
    {
        _human = new Player("Player");
        _computer = new Player("Computer");
    }

    // Main game loop
    public void Play()
    {
        Console.WriteLine("Hello this is Rock Paper Scissors");

        // Ask for number of rounds
        Console.Write("Enter number of rounds: ");
        if (!int.TryParse(Console.ReadLine(), out int totalRounds) || totalRounds <= 0)
        {
            Console.WriteLine("Invalid number of rounds. Exiting.");
            return;
        }
        _totalRounds = totalRounds;

        _human.ResetScore();
        _computer.ResetScore();

        int roundsPlayed = 0;

        while (roundsPlayed < _totalRounds)
        {
            // Early termination — winner already determined
            int roundsLeft = _totalRounds - roundsPlayed;
            if (_human.Score - _computer.Score > roundsLeft ||
                _computer.Score - _human.Score > roundsLeft)
                break;

            Console.Clear();
            Console.WriteLine($"Round {roundsPlayed + 1} of {_totalRounds}");
            Console.WriteLine($"Score — {_human.Name}: {_human.Score} | {_computer.Name}: {_computer.Score}");
            Console.WriteLine();
            Console.WriteLine("1 - Rock");
            Console.WriteLine("2 - Paper");
            Console.WriteLine("3 - Scissors");
            Console.WriteLine("4 - Well");
            Console.WriteLine("0 - Exit");

            // Get player move
            var playerMove = new Move();
            playerMove.ReadFromConsole();

            // Handle exit
            if (playerMove.Number == 0)
                return;

            // Skip round if invalid input
            if (!playerMove.IsValid())
            {
                Console.WriteLine("Invalid input. Round not counted. Press any key...");
                Console.ReadKey();
                continue;
            }

            // Generate computer move
            var computerMove = new Move();
            computerMove.GenerateRandom();

            // Determine round result
            string resultText = DetermineResult(playerMove, computerMove);
            var result = new GameResult(playerMove, computerMove, resultText);

            roundsPlayed++;
            result.Print();

            Console.WriteLine($"Score — {_human.Name}: {_human.Score} | {_computer.Name}: {_computer.Score}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Show final result
        ShowFinalResult();
    }

    // Determine winner of a round and update scores
    private string DetermineResult(Move playerMove, Move computerMove)
    {
        if (playerMove.Number == computerMove.Number)
            return "Draw!";

        bool playerWins =
            (playerMove.Number == 1 && computerMove.Number == 3) || // Rock beats Scissors
            (playerMove.Number == 2 && computerMove.Number == 1) || // Paper beats Rock
            (playerMove.Number == 3 && computerMove.Number == 2) || // Scissors beats Paper
            (playerMove.Number == 4 && computerMove.Number == 1) || // Well beats Rock
            (playerMove.Number == 4 && computerMove.Number == 3) || // Well beats Scissors
            (playerMove.Number == 2 && computerMove.Number == 4);   // Paper beats Well

        if (playerWins)
        {
            _human.AddPoint();
            return "You win this round!";
        }

        _computer.AddPoint();
        return "Computer wins this round!";
    }

    // Print final game result
    private void ShowFinalResult()
    {
        Console.Clear();
        Console.WriteLine("=== GAME OVER ===");
        Console.WriteLine($"Final Score — {_human.Name}: {_human.Score} | {_computer.Name}: {_computer.Score}");

        if (_human.Score > _computer.Score)
            Console.WriteLine("You won the game!");
        else if (_computer.Score > _human.Score)
            Console.WriteLine("Computer won the game!");
        else
            Console.WriteLine("It's a draw!");
    }
}