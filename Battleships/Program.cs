using System.Text.RegularExpressions;
using Battleships;

var grid = new Grid();
grid.AddShip(ShipType.Destroyer);
grid.AddShip(ShipType.Destroyer);
grid.AddShip(ShipType.Battleship);

Console.WriteLine("Welcome to Battleships!");

var numberOfShots = 0;
while (true)
{
    Console.WriteLine(grid.ToString());

    Console.Write($"Shot #{numberOfShots + 1}. Enter shot location: ");
    var shotLocationInput = Console.ReadLine();

    var shotLocation = ParseShotLocation(shotLocationInput);
    if (shotLocation is not null)
    {
        var shotResult = grid.Shoot(shotLocation.Value.Row, shotLocation.Value.Column);
        if (shotResult != ShotResult.CellIsAlreadyShot)
        {
            numberOfShots++;
        }

        Console.WriteLine(GetShotResultDescription(shotResult));

        if (grid.AreAllShipsDestroyed())
        {
            Console.WriteLine($"All ships are destroyed in {numberOfShots} shots!");
            Console.WriteLine(grid.ToString());
            break;
        }
    }
    else
    {
        Console.WriteLine("Shot location is not valid.");
    }
}

(int Row, int Column)? ParseShotLocation(string? shotLocation)
{
    var isShotLocationValid = shotLocation is not null &&
                              Regex.IsMatch(shotLocation, "^[a-jA-J]([\\d]|10)$");

    if (isShotLocationValid)
    {
        var row = shotLocation!.ToUpper()[0] - Constants.CharACode;
        var column = int.Parse(shotLocation.Substring(1, shotLocation.Length - 1)) - 1;
        return (row, column);
    }

    return null;
}

string GetShotResultDescription(ShotResult shotResult) =>
    shotResult switch
    {
        ShotResult.CellIsAlreadyShot => "The cell is already shot.",
        ShotResult.Miss => "Miss!",
        ShotResult.DestroyerHit => "The destroyer is hit!",
        ShotResult.DestroyerSunk => "The destroyer has sunk!",
        ShotResult.BattleshipHit => "The battleship is hit!",
        ShotResult.BattleshipSunk => "The battleship has sunk!",
        _ => throw new NotSupportedException(nameof(shotResult))
    };