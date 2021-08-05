namespace NonUnity.Game
{
    /// <summary>
    /// Обновляемая система
    /// </summary>
    public interface IUpdateSystem : ISystem
    {
        /// <summary>
        /// Обновление системы
        /// </summary>
        /// <param name="dt">Время с прошлого обновления</param>
        void Update(float dt);
    }
}