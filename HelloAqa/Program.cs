Console.Write("Enter number of rounds: ");
if (!int.TryParse(Console.ReadLine(), out int rounds) || rounds <= 0)
{
    Console.WriteLine("Invalid number of rounds. Exiting.");
    return;
}

var game = new Game(new Player("Nick"), new Player("Computer"), rounds);
game.Play();
