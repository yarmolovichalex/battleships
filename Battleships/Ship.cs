namespace Battleships;

public class Ship
{
    public IList<Cell> Cells { get; }
    
    public Ship(ICollection<Cell> cells)
    {
        if (Constants.ShipSizeByType.Values.All(x => x != cells.Count))
        {
            throw new ArgumentException("Invalid number of cells");
        }

        if (cells.Any(x => x.Ship is not null))
        {
            throw new ArgumentException("Some cells are already occupied");
        }

        Cells = cells.ToList();
        foreach (var cell in Cells)
        {
            cell.PlaceShip(this);
        }
    }

    public ShipType Type => Constants.ShipSizeByType.First(x => x.Value == Cells.Count).Key;
    public bool HasSunk => Cells.All(x => x.IsShot);
}