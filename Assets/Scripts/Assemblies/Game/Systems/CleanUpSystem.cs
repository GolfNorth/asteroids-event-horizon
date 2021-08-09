using System.Linq;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система очистки
    /// </summary>
    public sealed class CleanUpSystem : BaseSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр события перезапуска
        /// </summary>
        private readonly EcsFilter<RestartComponent> _restartFilter;

        /// <summary>
        /// Фильтр компонента столкновений
        /// </summary>
        private readonly EcsFilter<CollisionComponent> _collisionFilter;

        public CleanUpSystem(Game game) : base(game)
        {
            _restartFilter = new EcsFilter<RestartComponent>(World);
            _collisionFilter = new EcsFilter<CollisionComponent>(World);
        }

        public void Update(float dt)
        {
            if (_restartFilter.Entities.Count > 0)
            {
                uint[] entities = _restartFilter.Entities.ToArray();

                foreach (uint entity in entities)
                {
                    World.RemoveComponent<RestartComponent>(entity);
                }
            }

            if (_collisionFilter.Entities.Count > 0)
            {
                uint[] entities = _collisionFilter.Entities.ToArray();

                foreach (uint entity in entities)
                {
                    World.RemoveComponent<CollisionComponent>(entity);
                }
            }
        }
    }
}