using System.Collections.Immutable;

namespace Battleships;

public static class Constants
{
    public static int CharACode = 65;

    public static ImmutableDictionary<ShipType, int> ShipSizeByType = new Dictionary<ShipType, int>
    {
        { ShipType.Destroyer, 4 },
        { ShipType.Battleship, 5 }
    }.ToImmutableDictionary();
}