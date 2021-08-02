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
        public static int MaxEntitiesCount = 128;

        /// <summary>
        /// Число компонентов в пуле по умолчанию
        /// </summary>
        public static int DefaultComponentPoolCapacity = 64;
    }
}