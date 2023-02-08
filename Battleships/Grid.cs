﻿using System.Text;

namespace Battleships;

public class Grid
{
    private const byte Size = 10;

    private readonly List<Cell> _cells = new(Size * Size);
    private readonly Random _random = new();

    public Grid()
    {
        _cells.AddRange(
            Enumerable.Range(0, Size)
                .SelectMany(x => Enumerable.Range(0, Size)
                    .Select(y => new Cell
                    {
                        Row = x,
                        Column = y
                    }))
        );
    }

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
                    cell.HasShip
                        ? cell.IsShot ? 'x' : 'o'
                        : cell.IsShot ? '_' : '.'
                );
                sb.Append(' ');
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public void AddShip(ShipType shipType)
    {
        var ship = new Ship { Type = shipType };
        var shipSize = ship.Size;

        while (true)
        {
            var isRow = _random.Next(0, 2) == 0; // otherwise column

            var startRow = _random.Next(0, isRow ? Size : Size - shipSize);
            var startColumn = _random.Next(0, isRow ? Size - shipSize : Size);

            var shipCells = isRow
                ? GetRowShipCells(startRow, startColumn, startColumn + shipSize)
                : GetColumnShipCells(startColumn, startRow, startRow + shipSize);

            if (!shipCells.Any(x => x.HasShip))
            {
                foreach (var cell in shipCells)
                {
                    cell.Ship = ship;
                }

                ship.Cells = shipCells;

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

        cell.IsShot = true;

        if (cell.HasShip)
        {
            if (cell.Ship!.Cells.All(x => x.IsShot))
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

    public bool AreAllShipsDestroyed() => _cells.Where(x => x.HasShip).All(x => x.IsShot);

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