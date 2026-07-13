class Position
{
    public int X { get; set; }
    public int Y { get; }

    public Position(int x, int y)
    {
        // Пункт 1 — валидация координат
        // throw new ArgumentException — бросаем исключение если координаты отрицательные
        // ArgumentException — стандартный тип исключения для неверных аргументов
        if (x < 0 || y < 0)
            throw new ArgumentException($"Position coordinates cannot be negative: X={x}, Y={y}");

        X = x;
        Y = y;
    }
}