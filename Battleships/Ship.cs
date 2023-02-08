namespace Battleships;

public class Ship
{
    public ShipType Type { get; init; }
    public int Size => Type switch
    {
        ShipType.Destroyer => 4,
        ShipType.Battleship => 5,
        _ => throw new NotSupportedException(nameof(Type))
    };
    public IList<Cell>? Cells { get; set; }
}