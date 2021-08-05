using System.Numerics;

namespace NonUnity.Game
{
    /// <summary>
    /// Компонент передвижения
    /// </summary>
    public struct MovementComponent
    {
        /// <summary>
        /// Вектор направления
        /// </summary>
        public Vector2 Direction;

        /// <summary>
        /// Векто скорости
        /// </summary>
        public Vector2 Velocity;
    }
}