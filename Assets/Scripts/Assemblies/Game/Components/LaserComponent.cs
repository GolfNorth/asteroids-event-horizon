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
        /// Время окончания выстрела
        /// </summary>
        public float EndFire;

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