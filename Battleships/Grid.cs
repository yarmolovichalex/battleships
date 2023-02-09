using System.Text;

namespace Battleships;

public class Grid
{
    private const byte Size = 10;

    private readonly List<Cell> _cells = new(Size * Size);
    private readonly IList<Ship> _ships = new List<Ship>();
    private readonly Random _random = new();

    public Grid()
    {
        _cells.AddRange(
            Enumerable.Range(0, Size)
                .SelectMany(x => Enumerable.Range(0, Size)
                    .Select(y => new Cell(x, y)))
        );
    }

    public void AddShip(ShipType shipType)
    {
        var shipSize = Constants.ShipSizeByType[shipType];

        while (true)
        {
            var isRow = _random.Next(0, 2) == 0; // otherwise column

            var startRow = _random.Next(0, isRow ? Size : Size - shipSize);
            var startColumn = _random.Next(0, isRow ? Size - shipSize : Size);

            var shipCells = isRow
                ? GetRowShipCells(startRow, startColumn, startColumn + shipSize)
                : GetColumnShipCells(startColumn, startRow, startRow + shipSize);

            if (shipCells.All(x => !x.IsOccupied))
            {
                _ships.Add(new Ship(shipCells));
                break;
            }
        }
    }

    public ShotResult Shoot(int row, int column)
    {
        var cell = GetCell(row, column);
        if (cell.IsShot)
        {
            return ShotResult.CellIsAlreadyShot;
        }

        cell.Shoot();

        if (cell.IsOccupied)
        {
            if (cell.Ship!.HasSunk)
            {
                return cell.Ship.Type switch
                {
                    ShipType.Destroyer => ShotResult.DestroyerSunk,
                    ShipType.Battleship => ShotResult.BattleshipSunk,
                    _ => throw new NotSupportedException(nameof(cell.Ship.Type))
                };
            }
            else
            {
                return cell.Ship.Type switch
                {
                    ShipType.Destroyer => ShotResult.DestroyerHit,
                    ShipType.Battleship => ShotResult.BattleshipHit,
                    _ => throw new NotSupportedException(nameof(cell.Ship.Type))
                };
            }
        }

        return ShotResult.Miss;
    }

    public bool HaveAllShipsSunk() => _ships.All(x => x.HasSunk);

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append("  ");
        for (var i = 0; i < Size; i++)
        {
            sb.Append(i + 1);
            sb.Append(' ');
        }

        sb.AppendLine();

        foreach (var row in _cells.GroupBy(x => x.Row).OrderBy(x => x.Key))
        {
            sb.Append(Convert.ToChar(Constants.CharACode + row.Key));
            sb.Append(' ');

            foreach (var cell in row.OrderBy(x => x.Column))
            {
                sb.Append(
                    cell.IsShot
                        ? cell.IsOccupied ? 'x' : 'o'
                        : '.'
                );
                sb.Append(' ');
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private Cell GetCell(int row, int column) => _cells.First(x => x.Row == row && x.Column == column);

    private IList<Cell> GetRowShipCells(int row, int startIndex, int shipSize) => _cells
        .Where(x => x.Row == row &&
                    x.Column >= startIndex && x.Column < shipSize)
        .ToList();

    private IList<Cell> GetColumnShipCells(int column, int startIndex, int shipSize) => _cells
        .Where(x => x.Column == column &&
                    x.Row >= startIndex && x.Row < shipSize)
        .ToList();
}