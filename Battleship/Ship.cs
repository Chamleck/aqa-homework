// Пункт 1.2 — абстрактный класс, наследники HorizontalShip и VerticalShip
abstract class Ship
{
    public Position Position { get; }
    public int Length { get; }

    // Пункт 5 — выстрелы, которые попали в этот корабль
    private readonly List<Shot> _hits = new List<Shot>();
    public IReadOnlyList<Shot> Hits => _hits;

    // Пункт 5 — корабль потоплен когда попаданий столько же сколько длина
    public bool IsSunk => _hits.Count >= Length;

    protected Ship(Position position, int length)
    {
        if (length <= 0)
            throw new ArgumentException($"Ship length must be greater than 0: Length={length}");

        Position = position;
        Length = length;
    }

    public void RegisterHit(Shot shot)
    {
        _hits.Add(shot);
    }

    public abstract bool IsOnPosition(Position position);
    public abstract bool IsInsideBoard(int rows, int columns);

    // Пункт 2 — определяет пересекаются ли корабли
    public abstract bool Intersects(Ship other);
}