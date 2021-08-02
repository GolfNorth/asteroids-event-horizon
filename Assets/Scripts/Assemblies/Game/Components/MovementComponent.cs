using System.Numerics;

namespace NonUnity.Game
{
    /// <summary>
    /// Компонент передвижения
    /// </summary>
    public struct MovementComponent
    {
        /// <summary>
        /// Направление движения
        /// </summary>
        public Vector2 Direction;

        /// <summary>
        /// Вектор скорости
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// Текущая скорость
        /// </summary>
        public float Speed;
    }
}