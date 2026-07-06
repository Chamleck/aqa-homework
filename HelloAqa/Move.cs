class Move
{
    public int Number { get; private set; }

    // Пункт 4 — вычисляемое свойство через switch expression
    // нет private set для нейм — значение вычисляется каждый раз при обращении на основе намбер
    public string Name => Number switch
    {
        1 => "Rock",
        2 => "Paper",
        3 => "Scissors",
        4 => "Well",
        _ => "Unknown"
    };

    public void ReadFromConsole()
    {
        var input = Console.ReadLine();
        if (int.TryParse(input, out int number))
            Number = number;
    }

    public void GenerateRandom()
    {
        var random = new Random();
        Number = random.Next(1, 5);
    }

    // Пункт 2 — параметры с дефолтными значениями
    // min=1, max=4 — можно вызвать IsValid() без параметров
    // или IsValid(1, 3) для диапазона без Колодца
    public bool IsValid(int min = 1, int max = 4)
    {
        return Number >= min && Number <= max;
    }
}