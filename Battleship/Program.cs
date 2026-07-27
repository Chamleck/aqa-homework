var settings = new GameSettings(rows: 5, columns: 5, minShipLength: 1, maxShipLength: 3);

Ship[] userShips =
{
    new HorizontalShip(new Position(0, 0), 2),
    new VerticalShip(new Position(3, 1), 2)
};

var userBoard = new Board(5, 5, userShips);
var game = new Game(settings);
game.Play(userBoard);