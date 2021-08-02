namespace NonUnity.Game
{
    /// <summary>
    /// Компонент лазера
    /// </summary>
    public struct LaserComponent
    {
        /// <summary>
        /// Количество зарядов
        /// </summary>
        public int Charges;

        /// <summary>
        /// Время следующего выстрела
        /// </summary>
        public float Cooldown;
    }
}