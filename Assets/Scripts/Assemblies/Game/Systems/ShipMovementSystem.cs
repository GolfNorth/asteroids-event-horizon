using System;
using System.Numerics;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система передвижения корабля
    /// </summary>
    public sealed class ShipMovementSystem : BaseSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр компонента корабля
        /// </summary>
        private readonly EcsFilter<ShipComponent> _filter;

        /// <summary>
        /// Угловая скорость
        /// </summary>
        private readonly ShipSettings _shipSettings;

        public ShipMovementSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<ShipComponent>(World);
            _shipSettings = Game.Settings.Ship;
        }

        public void Update(float dt)
        {
            if (_filter.Entities.Count == 0)
                return;

            float rotation = Game.Command.Rotation;
            float translation = Game.Command.Translation;

            foreach (uint entityId in _filter.Entities)
            {
                ref MovementComponent movement = ref World.GetComponent<MovementComponent>(entityId);

                Rotate(ref movement, rotation, dt);
                Translate(ref movement, translation, dt);
            }
        }

        /// <summary>
        /// Поворот корабля
        /// </summary>
        private void Rotate(ref MovementComponent movement, float rotation, float dt)
        {
            if (rotation == 0)
                return;

            float deltaAngle = rotation * _shipSettings.AngularSpeed * dt;
            Matrix3x2 rotationMatrix = Matrix3x2.CreateRotation((float) Math.PI * deltaAngle / 180f);

            movement.Direction = Vector2.Transform(movement.Direction, rotationMatrix);
        }

        /// <summary>
        /// Передвижение корабля
        /// </summary>
        private void Translate(ref MovementComponent movement, float translation, float dt)
        {
            if (translation > 0)
            {
                movement.Velocity += movement.Direction * _shipSettings.LinearSpeed * dt;

                if (movement.Velocity.LengthSquared() > _shipSettings.LinearSpeed * _shipSettings.LinearSpeed)
                {
                    movement.Velocity = Vector2.Normalize(movement.Velocity) * _shipSettings.LinearSpeed;
                }
            }
            else
            {
                movement.Velocity = movement.Velocity.LengthSquared() >
                                    _shipSettings.StopSpeed * _shipSettings.StopSpeed
                    ? Vector2.Lerp(movement.Velocity, Vector2.Zero, (1 - _shipSettings.Inertia) * dt)
                    : Vector2.Zero;
            }
        }
    }
}