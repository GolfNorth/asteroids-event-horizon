namespace NonUnity.Game
{
    /// <summary>
    /// Система столкновения корабля
    /// </summary>
    public sealed class ShipCollisionSystem : ExecuteSystem<ShipComponent, CollisionComponent>
    {
        public ShipCollisionSystem(Game game) : base(game)
        {
        }

        protected override void Execute(uint entity, float dt)
        {
            World.AddComponent<DestroyComponent>(entity);

            Game.State = GameState.GameOver;
        }
    }
}