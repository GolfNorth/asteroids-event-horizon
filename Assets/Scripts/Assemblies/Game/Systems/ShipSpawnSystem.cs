using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система респауна корабля
    /// </summary>
    public sealed class ShipSpawnSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<RestartComponent> _filter;

        public ShipSpawnSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<RestartComponent>(World);
        }

        public void Update(float dt)
        {
            if (_filter.Entities.Count == 0)
                return;

            Game.Factory.CreateShip();
        }
    }
}