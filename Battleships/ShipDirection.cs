namespace Battleships;

public enum ShipDirection : byte
{
    Row,
    Column
}

public static class ShipDirectionExtensions
{
    public static bool IsRow(this ShipDirection value) => value == ShipDirection.Row;
    public static bool IsColumn(this ShipDirection value) => value == ShipDirection.Column;
}