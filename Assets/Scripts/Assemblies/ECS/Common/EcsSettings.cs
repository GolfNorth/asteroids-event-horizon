namespace NonUnity.Ecs
{
    /// <summary>
    /// Конфигуратор пространства сущностей
    /// </summary>
    public struct EcsSettings
    {
        /// <summary>
        /// Максимальное количество сущностей по умолчанию
        /// </summary>
        public const int DefaultMaxEntitiesCount = 128;

        /// <summary>
        /// Число компонентов в пуле по умолчанию
        /// </summary>
        public static int DefaultComponentPoolCapacity = 64;

        /// <summary>
        /// Максимальное количество сущностей
        /// </summary>
        public int MaxEntitiesCount;

        /// <summary>
        /// Число компонентов в пуле
        /// </summary>
        public int ComponentPoolCapacity;
    }
}