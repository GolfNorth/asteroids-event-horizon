namespace NonUnity.Game
{
    /// <summary>
    /// Интерфейс фабрики визаулизаторов
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// Создать корабль
        /// </summary>
        IView CreateShip();

        /// <summary>
        /// Создать пулю
        /// </summary>
        IView CreateBullet();

        /// <summary>
        /// Создать лазер
        /// </summary>
        IView CreateLaser();

        /// <summary>
        /// Создать астероид
        /// </summary>
        /// <param name="size">Размер астероида</param>
        IView CreateAsteroid(AsteroidSize size);

        /// <summary>
        /// Создать НЛО
        /// </summary>
        IView CreateUfo();
    }
}