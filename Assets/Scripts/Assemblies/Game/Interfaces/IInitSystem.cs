namespace NonUnity.Game
{
    /// <summary>
    /// Инициализируемая система
    /// </summary>
    public interface IInitSystem : ISystem
    {
        /// <summary>
        /// Инициализация системы
        /// </summary>
        void Init();
    }
}