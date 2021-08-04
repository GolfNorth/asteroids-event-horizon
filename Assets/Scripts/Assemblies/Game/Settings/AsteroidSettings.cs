using System;

namespace NonUnity.Game
{
    /// <summary>
    /// Конфигурация астероида в зависимости от размера
    /// </summary>
    [Serializable]
    public struct AsteroidSizeSettings
    {
        /// <summary>
        /// Радиус астероида
        /// </summary>
        public float Radius;

        /// <summary>
        /// Минимальная скорость
        /// </summary>
        public float MinSpeed;

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public float MaxSpeed;

        /// <summary>
        /// Конструктор конфигурации астероида
        /// </summary>
        /// <param name="radius">Радиус астероида</param>
        /// <param name="minSpeed">Минимальная скорость</param>
        /// <param name="maxSpeed">Максимальная скорость</param>
        public AsteroidSizeSettings(float radius, float minSpeed, float maxSpeed)
        {
            Radius = radius;
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
        }
    }

    /// <summary>
    /// Конфигурация астероидов
    /// </summary>
    [Serializable]
    public struct AsteroidSettings
    {
        /// <summary>
        /// Время задержки между появлением астероидов по умолчанию
        /// </summary>
        public const float DefaultSpawnDelay = 1.5f;

        /// <summary>
        /// Конфигурация астероидов в зависимости от размера по умолчанию
        /// </summary>
        public static readonly AsteroidSizeSettings[] DefaultSizeSettings = new[]
        {
            new AsteroidSizeSettings(0.5f, 3f, 4f),
            new AsteroidSizeSettings(0.75f, 2f, 3f),
            new AsteroidSizeSettings(1f, 1f, 2f)
        };

        /// <summary>
        /// Время задержки между появлением астероидов
        /// </summary>
        public float SpawnDelay;

        /// <summary>
        /// Конфигурация астероидов в зависимости от размера
        /// </summary>
        public AsteroidSizeSettings[] SizeSettings;
    }
}