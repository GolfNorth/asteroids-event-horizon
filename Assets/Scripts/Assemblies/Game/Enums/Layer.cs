using System;

namespace NonUnity.Game
{
    /// <summary>
    /// Слой сущности
    /// </summary>
    [Flags]
    public enum Layer : byte
    {
        None = 0,
        Ship = 1,
        Ufo = 2,
        Asteroid = 4,
        Enemy = Ufo | Asteroid
    }
}