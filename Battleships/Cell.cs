namespace Battleships;

public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public Ship? Ship { get; set; }
    public bool IsShot { get; set; }

    public bool HasShip => Ship is not null;
}