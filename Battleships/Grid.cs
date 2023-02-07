using System.Text;

namespace Battleships;

public class Grid
{
    private const byte Size = 10;
    private readonly bool[,] _grid = new bool[Size, Size];

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
            sb.Append(Convert.ToChar(i + 65));
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
}