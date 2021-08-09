namespace NonUnity.Game
{
    /// <summary>
    /// Система обработки столкновения НЛО
    /// </summary>
    public sealed class UfoCollisionSystem : ExecuteSystem<UfoComponent, CollisionComponent>
    {
        public UfoCollisionSystem(Game game) : base(game)
        {
        }

        protected override void Execute(uint entity, float dt)
        {
            ref CollisionComponent collision = ref World.GetComponent<CollisionComponent>(entity);

            if (World.HasComponent<ShipComponent>(collision.Other))
                return;

            Game.Score++;

            World.AddComponent<DestroyComponent>(entity);
        }
    }
}