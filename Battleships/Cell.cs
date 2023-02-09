namespace Battleships;

public class Cell
{
    private Ship? _ship;
    private bool _isShot;

    public int Row { get; }
    public int Column { get; }
    public Ship? Ship => _ship;
    public bool IsShot => _isShot;
    public bool IsOccupied => Ship is not null;

    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public void PlaceShip(Ship ship)
    {
        if (_ship is not null)
        {
            throw new InvalidOperationException("The cell is already occupied");
        }

        _ship = ship;
    }

    public void Shoot()
    {
        if (_isShot)
        {
            throw new InvalidOperationException("The cell is already shot");
        }

        _isShot = true;
    }
}