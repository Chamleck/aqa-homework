interface IShooter
{
    Shot Shoot(Board targetBoard, IReadOnlyList<Shot> shotHistory);
}