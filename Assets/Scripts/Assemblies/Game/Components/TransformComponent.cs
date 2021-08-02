using System.Numerics;

namespace NonUnity.Game
{
    /// <summary>
    /// Компонент трансформа
    /// </summary>
    public struct TransformComponent
    {
        /// <summary>
        /// Позиция сущности
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Угол поворота сущности
        /// </summary>
        public float Rotation;
    }
}