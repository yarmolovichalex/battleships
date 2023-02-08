namespace Battleships;

public enum ShotResult : byte
{
    CellIsAlreadyShot,
    Miss,
    DestroyerHit,
    DestroyerSunk,
    BattleshipHit,
    BattleshipSunk
}