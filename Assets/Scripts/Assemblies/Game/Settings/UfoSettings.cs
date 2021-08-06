using System;
using System.Numerics;

namespace NonUnity.Game
{
    /// <summary>
    /// Конфигурация НЛО
    /// </summary>
    [Serializable]
    public struct UfoSettings
    {
        /// <summary>
        /// Отступ от границы экрана по умолчанию
        /// </summary>
        public const float DefaultOffset = 2f;

        /// <summary>
        /// Задержка между появлениям НЛО по умолчанию
        /// </summary>
        public const float DefaultSpawnDelay = 4f;

        /// <summary>
        /// Линейная скорость НЛО по умолчанию
        /// </summary>
        public const float DefaultLinearSpeed = 4f;

        /// <summary>
        /// Вершины формы НЛО по умолчанию
        /// </summary>
        public static readonly Vector2[] DefaultShapeVertexes = new[]
        {
            new Vector2(1f, 0f), new Vector2(0f, -0.5f), new Vector2(-1f, 0f), new Vector2(0f, 0.5f)
        };

        /// <summary>
        /// Отступ от границы экрана
        /// </summary>
        public float Offset;

        /// <summary>
        /// Задержка между появлениям НЛО
        /// </summary>
        public float SpawnDelay;

        /// <summary>
        /// Линейная скорость НЛО
        /// </summary>
        public float LinearSpeed;

        /// <summary>
        /// Вершины формы НЛО
        /// </summary>
        public Vector2[] ShapeVertexes;
    }
}