namespace NonUnity.Game
{
    /// <summary>
    /// Система обновления форм
    /// </summary>
    public sealed class ShapeSystem : ExecuteSystem<TransformComponent, BodyComponent>
    {
        public ShapeSystem(Game game) : base(game)
        {
        }

        protected override void Execute(uint entity, float dt)
        {
            ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
            ref BodyComponent body = ref World.GetComponent<BodyComponent>(entity);

            body.Shape.Set(transform.Position, transform.Rotation);
        }
    }
}