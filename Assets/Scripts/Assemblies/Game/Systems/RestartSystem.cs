using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система рестарта
    /// </summary>
    public abstract class RestartSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр рестарта
        /// </summary>
        private readonly EcsFilter<RestartComponent> _restartFilter;

        protected RestartSystem(Game game) : base(game)
        {
            _restartFilter = new EcsFilter<RestartComponent>(World);
        }

        public virtual void Update(float dt)
        {
            if (_restartFilter.Entities.Count > 0)
            {
                Restart();
            }
        }

        /// <summary>
        /// Рестарт игры
        /// </summary>
        protected abstract void Restart();
    }
}