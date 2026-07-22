// Пункт 3 — класс хранит информацию об одном выстреле
class Shot
{
    // Доска по которой был сделан выстрел
    public Board Board { get; }
    
    // Позиция выстрела
    public Position Position { get; }
    
    // Корабль в который попали — null если промах
    // Ship? — nullable тип, знак '?' означает что значение может быть null
    public Ship? Ship { get; }

    public Shot(Board board, Position position, Ship? ship)
    {
        Board = board;
        Position = position;
        Ship = ship;
    }

    // Удобное свойство — попадание или промах
    // true если Ship не null (попали в корабль)
    public bool IsHit => Ship != null;
}