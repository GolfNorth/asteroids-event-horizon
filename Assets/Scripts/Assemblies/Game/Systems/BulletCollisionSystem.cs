namespace NonUnity.Game
{
    /// <summary>
    /// Система столкновения пуль
    /// </summary>
    public sealed class BulletCollisionSystem : ExecuteSystem<BulletComponent, CollisionComponent>
    {
        public BulletCollisionSystem(Game game) : base(game)
        {
        }

        protected override void Execute(uint entity, float dt)
        {
            World.AddComponent<DestroyComponent>(entity);
        }
    }
}