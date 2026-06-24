Console.WriteLine("Hello this is Rock Paper Scissors");

// 1. Запрашиваем количество раундов
Console.Write("Enter number of rounds: ");
if (!int.TryParse(Console.ReadLine(), out int totalRounds) || totalRounds <= 0)
{
    Console.WriteLine("Invalid number of rounds. Exiting.");
    return;
}

int playerScore = 0;
int computerScore = 0;
int roundsPlayed = 0;

while (roundsPlayed < totalRounds)
{
    // 5. Досрочное завершение — победитель уже определён
    int roundsLeft = totalRounds - roundsPlayed;
    if (playerScore - computerScore > roundsLeft || computerScore - playerScore > roundsLeft)
        break;

    Console.Clear();
    Console.WriteLine($"Round {roundsPlayed + 1} of {totalRounds}");
    Console.WriteLine($"Score — You: {playerScore} | Computer: {computerScore}");
    Console.WriteLine();
    Console.WriteLine("1 - Rock");
    Console.WriteLine("2 - Paper");
    Console.WriteLine("3 - Scissors");
    Console.WriteLine("4 - Well");  // 4. Новый ход
    Console.WriteLine("0 - Exit");

    var userInput = Console.ReadLine();

    // 2. Не считаем раунд при некорректном вводе
    if (!int.TryParse(userInput, out int userChoice) || !(userChoice >= 0 && userChoice <= 4))
    {
        Console.WriteLine("Invalid input. Round not counted. Press any key...");
        Console.ReadKey();
        continue;
    }

    if (userChoice == 0)
        return;

    var random = new Random();
    var computerChoice = random.Next(1, 5); // 1-4

    string userChoiceString = userChoice switch
    {
        1 => "Rock",
        2 => "Paper",
        3 => "Scissors",
        _ => "Well"
    };

    string computerChoiceString = computerChoice switch
    {
        1 => "Rock",
        2 => "Paper",
        3 => "Scissors",
        _ => "Well"
    };

    Console.WriteLine($"You chose: {userChoiceString}");
    Console.WriteLine($"Computer chose: {computerChoiceString}");

    // 4. Логика с Колодцем: Well бьёт Rock и Scissors, Paper бьёт Well
    bool playerWins =
        (userChoice == 1 && computerChoice == 3) || // Rock beats Scissors
        (userChoice == 2 && computerChoice == 1) || // Paper beats Rock
        (userChoice == 3 && computerChoice == 2) || // Scissors beats Paper
        (userChoice == 4 && computerChoice == 1) || // Well beats Rock
        (userChoice == 4 && computerChoice == 3) || // Well beats Scissors
        (userChoice == 2 && computerChoice == 4);   // Paper beats Well

    bool computerWins =
        (computerChoice == 1 && userChoice == 3) ||
        (computerChoice == 2 && userChoice == 1) ||
        (computerChoice == 3 && userChoice == 2) ||
        (computerChoice == 4 && userChoice == 1) ||
        (computerChoice == 4 && userChoice == 3) ||
        (computerChoice == 2 && userChoice == 4);

    roundsPlayed++;

    if (playerWins)
    {
        playerScore++;
        Console.WriteLine("You win this round!");
    }
    else if (computerWins)
    {
        computerScore++;
        Console.WriteLine("Computer wins this round!");
    }
    else
    {
        Console.WriteLine("Draw!");
    }

    // 3. Счёт после каждого раунда
    Console.WriteLine($"Score — You: {playerScore} | Computer: {computerScore}");
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

// Итог игры
Console.Clear();
Console.WriteLine("=== GAME OVER ===");
Console.WriteLine($"Final Score — You: {playerScore} | Computer: {computerScore}");

if (playerScore > computerScore)
    Console.WriteLine("You won the game!");
else if (computerScore > playerScore)
    Console.WriteLine("Computer won the game!");
else
    Console.WriteLine("It's a draw!");