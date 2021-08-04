using System;
using System.Numerics;

namespace NonUnity.Game
{
    /// <summary>
    /// Конфигурация корабля
    /// </summary>
    [Serializable]
    public struct ShipSettings
    {
        /// <summary>
        /// Угловая скорость по умолчанию
        /// </summary>
        public const float DefaultAngularSpeed = 280f;

        /// <summary>
        /// Линейная скорость по умолчанию
        /// </summary>
        public const float DefaultLinearSpeed = 8f;

        /// <summary>
        /// Скорость для полной остановки по умолчанию
        /// </summary>
        public const float DefaultStopSpeed = 0.6f;

        /// <summary>
        /// Инерция по умолчанию
        /// </summary>
        public const float DefaultInertia = 0.6f;

        /// <summary>
        /// Скорость полета пуль по умолчанию
        /// </summary>
        public const float DefaultBulletSpeed = 12f;

        /// <summary>
        /// Скорострельность пуль по умолчанию
        /// </summary>
        public const float DefaultBulletFireRate = 0.25f;

        /// <summary>
        /// Скорострельность лазера по умолчанию
        /// </summary>
        public const float DefaultLaserFireRate = 3f;

        /// <summary>
        /// Продолжительность лазера по умолчанию
        /// </summary>
        public const float DefaultLaserFireDuration = 2f;

        /// <summary>
        /// Количество зарядов лазера по умолчанию
        /// </summary>
        public const int DefaultLaserMaxCharges = 3;

        /// <summary>
        /// Время перезарядки лазера по умолчанию 
        /// </summary>
        public const float DefaultLaserCooldown = 4f;

        /// <summary>
        /// Вершины формы по умолчанию
        /// </summary>
        public static readonly Vector2[] DefaultShapeVertexes = new[]
        {
            new Vector2(0.5f, 0f), new Vector2(-0.5f, -0.25f), new Vector2(-0.5f, 0.25f)
        };

        /// <summary>
        /// Угловая скорость
        /// </summary>
        public float AngularSpeed;

        /// <summary>
        /// Линейная скорость
        /// </summary>
        public float LinearSpeed;

        /// <summary>
        /// Скорость для полной остановки
        /// </summary>
        public float StopSpeed;

        /// <summary>
        /// Инерция
        /// </summary>
        public float Inertia;

        /// <summary>
        /// Скорость полета пуль
        /// </summary>
        public float BulletSpeed;

        /// <summary>
        /// Скорострельность пуль
        /// </summary>
        public float BulletFireRate;

        /// <summary>
        /// Скорострельность лазера
        /// </summary>
        public float LaserFireRate;

        /// <summary>
        /// Продолжительность лазера
        /// </summary>
        public float LaserFireDuration;

        /// <summary>
        /// Количество зарядов лазера
        /// </summary>
        public int LaserMaxCharges;

        /// <summary>
        /// Время перезарядки лазера
        /// </summary>
        public float LaserCooldown;

        /// <summary>
        /// Вершины формы
        /// </summary>
        public Vector2[] ShapeVertexes;
    }
}