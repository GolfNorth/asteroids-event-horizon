using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система столкновения пуль
    /// </summary>
    public sealed class BulletCollisionSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<BulletComponent, CollisionComponent> _filter;

        public BulletCollisionSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<BulletComponent, CollisionComponent>(World);
        }

        public void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                World.AddComponent<DestroyComponent>(entity);
            }
        }
    }
}