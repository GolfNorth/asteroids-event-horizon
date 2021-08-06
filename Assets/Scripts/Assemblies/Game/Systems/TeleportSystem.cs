using System.Drawing;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система телепортирования сущностей
    /// </summary>
    public sealed class TeleportSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<TransformComponent> _filter;

        public TeleportSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<TransformComponent>(World);
        }

        public void Update(float dt)
        {
            RectangleF bounds = Game.Bounds;

            foreach (uint entity in _filter.Entities)
            {
                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);

                if (transform.Position.X < bounds.Left - transform.Offset)
                    transform.Position.X = bounds.Right + transform.Offset;

                if (transform.Position.X > bounds.Right + transform.Offset)
                    transform.Position.X = bounds.Left - transform.Offset;

                if (transform.Position.Y < bounds.Bottom - transform.Offset)
                    transform.Position.Y = bounds.Top + transform.Offset;

                if (transform.Position.Y > bounds.Top + transform.Offset)
                    transform.Position.Y = bounds.Bottom - transform.Offset;
            }
        }
    }
}