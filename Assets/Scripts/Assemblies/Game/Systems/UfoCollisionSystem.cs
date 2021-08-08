using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система обработки столкновения НЛО
    /// </summary>
    public sealed class UfoCollisionSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<UfoComponent, CollisionComponent> _filter;

        public UfoCollisionSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<UfoComponent, CollisionComponent>(World);
        }

        public void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                ref CollisionComponent collision = ref World.GetComponent<CollisionComponent>(entity);

                if (World.HasComponent<ShipComponent>(collision.Other))
                    continue;

                Game.Score++;

                World.AddComponent<DestroyComponent>(entity);
            }
        }
    }
}