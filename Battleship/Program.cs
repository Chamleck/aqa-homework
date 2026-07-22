var shipPosition = new Position(2, 1);
var ship = new Ship(shipPosition, 2);
var board = new Board(5, 5, ship);
var game = new Game();
game.Play(board);