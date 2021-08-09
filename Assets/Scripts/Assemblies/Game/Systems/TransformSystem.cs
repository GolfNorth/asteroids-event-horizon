using System;

namespace NonUnity.Game
{
    /// <summary>
    /// Система обновления трансформов
    /// </summary>
    public sealed class TransformSystem : ExecuteSystem<TransformComponent, MovementComponent>
    {
        public TransformSystem(Game game) : base(game)
        {
        }

        protected override void Execute(uint entity, float dt)
        {
            ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
            ref MovementComponent movement = ref World.GetComponent<MovementComponent>(entity);

            transform.Rotation = (float) (Math.Atan2(movement.Direction.Y, movement.Direction.X) * (180 / Math.PI));
            transform.Position += movement.Velocity * dt;
        }
    }
}