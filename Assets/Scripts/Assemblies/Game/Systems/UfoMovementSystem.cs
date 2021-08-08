using System.Numerics;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система передвижения НЛО
    /// </summary>
    public sealed class UfoMovementSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей НЛО
        /// </summary>
        private readonly EcsFilter<UfoComponent> _ufoFilter;

        /// <summary>
        /// Фильтр сущностей НЛО
        /// </summary>
        private readonly EcsFilter<ShipComponent> _shipFilter;

        public UfoMovementSystem(Game game) : base(game)
        {
            _ufoFilter = new EcsFilter<UfoComponent>(World);
            _shipFilter = new EcsFilter<ShipComponent>(World);
        }

        public void Update(float dt)
        {
            if (Game.State != GameState.Play)
                return;

            uint shipEntity = 0;

            foreach (uint entity in _shipFilter.Entities)
            {
                shipEntity = entity;

                break;
            }

            ref TransformComponent shipTransform = ref World.GetComponent<TransformComponent>(shipEntity);

            foreach (uint entity in _ufoFilter.Entities)
            {
                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
                ref MovementComponent movement = ref World.GetComponent<MovementComponent>(entity);

                movement.Velocity = Vector2.Normalize(shipTransform.Position - transform.Position) *
                                    Game.Settings.Ufo.LinearSpeed;
            }
        }
    }
}