using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система лазерного выстрела
    /// </summary>
    public sealed class LaserSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<LaserComponent> _filter;

        public LaserSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<LaserComponent>(World);
        }

        public void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                if (Game.State == GameState.GameOver)
                {
                    World.AddComponent<DeactivateComponent>(entity);

                    continue;
                }

                ref LaserComponent laser = ref World.GetComponent<LaserComponent>(entity);

                laser.EndFire -= dt;

                if (laser.EndFire <= 0)
                {
                    World.AddComponent<DeactivateComponent>(entity);

                    continue;
                }

                ref TransformComponent ownerTransform = ref World.GetComponent<TransformComponent>(laser.Owner);
                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);

                transform.Position = ownerTransform.Position;
                transform.Rotation = ownerTransform.Rotation;
            }
        }
    }
}