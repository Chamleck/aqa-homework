// IPlayer наследует IShooter — Game работает только с IPlayer, но может вызывать Shoot()
interface IPlayer : IShooter
{
    string Name { get; }
    Board Board { get; }
}