namespace NonUnity.Game
{
    /// <summary>
    /// Компонент команд
    /// </summary>
    public struct CommandComponent
    {
        /// <summary>
        /// Передвижение корабля
        /// </summary>
        public float Translation;

        /// <summary>
        /// Поворот корабля
        /// </summary>
        public float Rotation;

        /// <summary>
        /// Выстрел корабля
        /// </summary>
        public bool Fire;

        /// <summary>
        /// Альтернативный выстрел корабля
        /// </summary>
        public bool AltFire;
    }
}