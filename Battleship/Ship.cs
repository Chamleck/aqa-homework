class Ship
{
    public Position Position { get; }
    public int Length { get; }

    public Ship(Position position, int length)
    {
        // Пункт 1 — валидация длины корабля
        // длина должна быть больше 0, отрицательный или нулевой корабль не имеет смысла
        if (length <= 0)
            throw new ArgumentException($"Ship length must be greater than 0: Length={length}");

        // position не должна быть null — нельзя создать корабль без позиции
        if (position == null)
            throw new ArgumentException("Ship position cannot be null");

        Position = position;
        Length = length;
    }
}