namespace NonUnity.Game
{
    /// <summary>
    /// Компонент лазерной пушки
    /// </summary>
    public struct LaserGunComponent
    {
        /// <summary>
        /// Количество зарядов
        /// </summary>
        public int Charges;

        /// <summary>
        /// Время готовности выстрела
        /// </summary>
        public float NextFire;

        /// <summary>
        /// Время перезарядки заряда
        /// </summary>
        public float NextCharge;
    }
}