using System.Drawing;

namespace NonUnity.Game
{
    /// <summary>
    /// Конфигурация игры
    /// </summary>
    public struct GameSettings
    {
        /// <summary>
        /// Игровые границы
        /// </summary>
        public RectangleF Bounds;

        /// <summary>
        /// Конфигурация корабля
        /// </summary>
        public ShipSettings Ship;

        /// <summary>
        /// Конфигурация атероидов по умолчанию
        /// </summary>
        public AsteroidSettings Asteroid;

        /// <summary>
        /// Конфигурация НЛО
        /// </summary>
        public UfoSettings Ufo;
    }
}