using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система перемещения визуализаторов
    /// </summary>
    public sealed class ViewMovementSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<TransformComponent, ViewComponent> _filter;

        public ViewMovementSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<TransformComponent, ViewComponent>(World);
        }

        public void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
                ref ViewComponent view = ref World.GetComponent<ViewComponent>(entity);

                view.Value.Position = transform.Position;
                view.Value.Rotation = transform.Rotation;
            }
        }
    }
}