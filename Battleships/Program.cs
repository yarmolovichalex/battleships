using Battleships;

var grid = new Grid();
grid.AddShip(Constants.ShipSize.Battleship);
grid.AddShip(Constants.ShipSize.Destroyer);
grid.AddShip(Constants.ShipSize.Destroyer);

Console.WriteLine(grid.ToString());
