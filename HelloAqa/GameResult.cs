class GameResult
{
    public Move PlayerMove { get; private set; }
    public Move ComputerMove { get; private set; }
    public string ResultText { get; private set; }

    // Пункт 1 — конструктор принимает оба хода и текст результата
    public GameResult(Move playerMove, Move computerMove, string resultText)
    {
        PlayerMove = playerMove;
        ComputerMove = computerMove;
        ResultText = resultText;
    }

    // Пункт 6 — перегрузка Print() без параметров
    public void Print()
    {
        Console.WriteLine($"You chose: {PlayerMove.Name}");
        Console.WriteLine($"Computer chose: {ComputerMove.Name}");
        Console.WriteLine(ResultText);
    }

    // Пункт 6 — перегрузка Print() с номером раунда
    public void Print(int roundNumber)
    {
        Console.WriteLine($"--- Round {roundNumber} ---");
        Console.WriteLine($"You chose: {PlayerMove.Name}");
        Console.WriteLine($"Computer chose: {ComputerMove.Name}");
        Console.WriteLine(ResultText);
    }
}