namespace NonUnity.Game
{
    /// <summary>
    /// Система перемещения визуализаторов
    /// </summary>
    public sealed class ViewMovementSystem : ExecuteSystem<TransformComponent, ViewComponent>
    {
        public ViewMovementSystem(Game game) : base(game)
        {
        }

        protected override void Execute(uint entity, float dt)
        {
            ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
            ref ViewComponent view = ref World.GetComponent<ViewComponent>(entity);

            view.Value.Position = transform.Position;
            view.Value.Rotation = transform.Rotation;
        }
    }
}