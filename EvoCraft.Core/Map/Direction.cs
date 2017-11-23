namespace EvoCraft.Core
{
    /// <summary>
    /// Egy irány. Jelenleg csak négy irányba lehet mozogni, de a lövedékhez szerettem volna átlós mozgást is, csak nem sikerült.
    /// </summary>
    public enum Direction {
        None,
        Up,
        Left,
        Down,
        Right,
        RightUp,
        RightDown,
        LeftUp,
        LeftDown
    }
}
