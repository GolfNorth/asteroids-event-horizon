using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система столкновения корабля
    /// </summary>
    public sealed class ShipCollisionSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<ShipComponent, CollisionComponent> _filter;

        public ShipCollisionSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<ShipComponent, CollisionComponent>(World);
        }

        public void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                World.AddComponent<DestroyComponent>(entity);

                Game.State = GameState.GameOver;
            }
        }
    }
}