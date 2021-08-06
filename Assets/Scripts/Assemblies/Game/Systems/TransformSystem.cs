using System;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система обновления трансформов
    /// </summary>
    public sealed class TransformSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<TransformComponent, MovementComponent> _filter;

        public TransformSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<TransformComponent, MovementComponent>(World);
        }

        public void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
                ref MovementComponent movement = ref World.GetComponent<MovementComponent>(entity);

                transform.Rotation = (float) (Math.Atan2(movement.Direction.Y, movement.Direction.X) * (180 / Math.PI));
                transform.Position += movement.Velocity * dt;
            }
        }
    }
}