// Пункт 1.4 — record вместо class, Result вычисляется из Ship
record Shot(Board Board, Position Position, Ship? Ship)
{
    public ShootResult Result => Ship != null ? ShootResult.Hit : ShootResult.Miss;
}