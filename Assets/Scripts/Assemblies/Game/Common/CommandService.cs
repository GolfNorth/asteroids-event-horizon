namespace NonUnity.Game
{
    /// <summary>
    /// Сервис команд
    /// </summary>
    public sealed class CommandService
    {
        /// <summary>
        /// Передвижение корабля
        /// </summary>
        internal float Translation { get; private set; }

        /// <summary>
        /// Поворот корабля
        /// </summary>
        internal float Rotation { get; private set; }

        /// <summary>
        /// Выстрел корабля
        /// </summary>
        internal bool Fire { get; private set; }

        /// <summary>
        /// Альтернативный выстрел корабля
        /// </summary>
        internal bool AltFire { get; private set; }

        /// <summary>
        /// Передвижение корабля
        /// </summary>
        public void Translate(float value)
        {
            Translation = value;
        }

        /// <summary>
        /// Поворот корабля
        /// </summary>
        public void Rotate(float value)
        {
            Rotation = value;
        }

        /// <summary>
        /// Выстрел корабля
        /// </summary>
        public void Shot(bool value)
        {
            Fire = value;
        }

        /// <summary>
        /// Альтернативный выстрел корабля
        /// </summary>
        public void AltShot(bool value)
        {
            AltFire = value;
        }
    }
}