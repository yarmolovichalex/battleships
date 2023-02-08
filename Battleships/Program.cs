using System.Text.RegularExpressions;
using Battleships;

var grid = new Grid();
grid.AddShip(Constants.ShipSize.Battleship);
grid.AddShip(Constants.ShipSize.Destroyer);
grid.AddShip(Constants.ShipSize.Destroyer);

Console.WriteLine("Welcome to Battleships!");

Console.WriteLine(grid.ToString());

var numberOfShots = 0;
while (true)
{
    Console.Write($"Shot #{numberOfShots + 1}. Enter shot location: ");
    var shotLocation = Console.ReadLine();

    if (IsShotLocationValid(shotLocation))
    {
        numberOfShots++;

        var row = shotLocation!.ToUpper()[0] - Constants.CharACode;
        var column = int.Parse(shotLocation[1].ToString()) - 1;

        var isShipHit = grid.Shoot(row, column);
        Console.WriteLine(isShipHit ? "Hit!" : "Miss!");
    
        Console.WriteLine(grid.ToString());

        if (grid.IsCleared())
        {
            Console.WriteLine($"All ships are destroyed in {numberOfShots} shots!");
            break;
        }
    }
    else
    {
        Console.WriteLine("Shot location is not valid.");
    }
}

bool IsShotLocationValid(string? shotLocation) => shotLocation is not null && 
                                                  Regex.IsMatch(shotLocation, "^[a-zA-Z][\\d]$");