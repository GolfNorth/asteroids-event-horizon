namespace NonUnity.Ecs
{
    /// <summary>
    /// Конфигуратор модуля
    /// </summary>
    public static class EcsConfig
    {
        /// <summary>
        /// Максимальное количество сущностей
        /// </summary>
        public const int MaxEntitiesCount = 128;

        /// <summary>
        /// Число компонентов в пуле по умолчанию
        /// </summary>
        public const int DefaultComponentPoolCapacity = 64;
    }
}