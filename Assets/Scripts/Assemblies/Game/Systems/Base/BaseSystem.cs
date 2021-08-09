using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Базовый класс системы
    /// </summary>
    public abstract class BaseSystem : ISystem
    {
        /// <summary>
        /// Геймплейный объект
        /// </summary>
        protected Game Game { get; }

        /// <summary>
        /// Пространство сущностей
        /// </summary>
        protected EcsWorld World => Game.World;

        /// <summary>
        /// Конструктор системы
        /// </summary>
        /// <param name="game">Геймплейный объект</param>
        protected BaseSystem(Game game)
        {
            Game = game;
        }
    }
}