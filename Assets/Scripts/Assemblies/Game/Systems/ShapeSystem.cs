using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система обновления форм
    /// </summary>
    public sealed class ShapeSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<TransformComponent, BodyComponent> _filter;

        public ShapeSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<TransformComponent, BodyComponent>(World);
        }

        public void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
                ref BodyComponent body = ref World.GetComponent<BodyComponent>(entity);

                body.Shape.Set(transform.Position, transform.Rotation);
            }
        }
    }
}