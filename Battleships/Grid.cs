using System.Text;

namespace Battleships;

public class Grid
{
    private const byte Size = 10;
    private readonly bool[,] _grid = new bool[Size, Size];
    private readonly Random _random = new();

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
        
        for (var i = 0; i < Size; i++)
        {
            sb.Append(Convert.ToChar(Constants.CharACode + i));
            sb.Append(' ');
            
            for (var j = 0; j < Size; j++)
            {
                sb.Append(_grid[i, j] ? 'x' : '.');
                sb.Append(' ');
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public void AddShip(int shipSize)
    {
        while (true)
        {
            var direction = (ShipDirection) _random.Next(0, Enum.GetNames(typeof(ShipDirection)).Length);
            var isRow = direction.IsRow();

            var startRow = _random.Next(0, isRow ? Size : Size - shipSize);
            var startColumn = _random.Next(0, isRow ? Size - shipSize : Size);

            var hasCollision = false;

            if (isRow)
            {
                for (var i = 0; i < shipSize; i++)
                {
                    if (_grid[startRow, startColumn + i])
                    {
                        hasCollision = true;
                    }
                }

                if (!hasCollision)
                {
                    for (var i = 0; i < shipSize; i++)
                    {
                        _grid[startRow, startColumn + i] = true;
                    }

                    break;
                }
            }
            else
            {
                for (var i = 0; i < shipSize; i++)
                {
                    if (_grid[startRow + i, startColumn])
                    {
                        hasCollision = true;
                    }
                }

                if (!hasCollision)
                {
                    for (var i = 0; i < shipSize; i++)
                    {
                        _grid[startRow + i, startColumn] = true;
                    }

                    break;
                }
            }
        }
    }
}