namespace Battleships;

public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool HasShip { get; set; }
    public bool IsShot { get; set; }
}