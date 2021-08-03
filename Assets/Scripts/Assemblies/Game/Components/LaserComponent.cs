namespace NonUnity.Game
{
    /// <summary>
    /// Компонент лазера
    /// </summary>
    public struct LaserComponent
    {
        /// <summary>
        /// Родительская сущность
        /// </summary>
        public uint Owner;

        /// <summary>
        /// Время окончания выстрела
        /// </summary>
        public float EndFire;
    }
}