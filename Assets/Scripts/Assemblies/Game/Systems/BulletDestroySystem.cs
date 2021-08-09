using System.Drawing;
using System.Numerics;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система утилизации пуль
    /// </summary>
    public sealed class BulletDestroySystem : BaseSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<BulletComponent> _filter;

        /// <summary>
        /// Отступ от игровой границы
        /// </summary>
        private readonly float _offset;

        public BulletDestroySystem(Game game) : base(game)
        {
            _filter = new EcsFilter<BulletComponent>(World);
            _offset = Game.Settings.Ship.BulletOffset;
        }

        public void Update(float dt)
        {
            if (_filter.Entities.Count == 0)
                return;

            RectangleF bounds = Game.Bounds;

            foreach (uint entity in _filter.Entities)
            {
                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);

                bool outBounds = IsOutBounds(in transform.Position, in bounds);

                if (outBounds)
                {
                    World.RemoveComponent<BodyComponent>(entity);
                    World.AddComponent<DeactivateComponent>(entity);
                }
            }
        }

        /// <summary>
        /// Нахождение вне игровой границы
        /// </summary>
        private bool IsOutBounds(in Vector2 position, in RectangleF bounds)
        {
            if (position.X < bounds.Left - _offset)
                return true;

            if (position.X > bounds.Right + _offset)
                return true;

            if (position.Y < bounds.Bottom - _offset)
                return true;

            if (position.Y > bounds.Top + _offset)
                return true;

            return false;
        }
    }
}