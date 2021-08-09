namespace NonUnity.Game
{
    /// <summary>
    /// Система лазерного выстрела
    /// </summary>
    public sealed class LaserSystem : ExecuteSystem<LaserComponent>
    {
        public LaserSystem(Game game) : base(game)
        {
        }

        protected override void Execute(uint entity, float dt)
        {
            if (Game.State == GameState.GameOver)
            {
                World.AddComponent<DeactivateComponent>(entity);

                return;
            }

            ref LaserComponent laser = ref World.GetComponent<LaserComponent>(entity);

            laser.EndFire -= dt;

            if (laser.EndFire <= 0)
            {
                World.AddComponent<DeactivateComponent>(entity);

                return;
            }

            ref TransformComponent ownerTransform = ref World.GetComponent<TransformComponent>(laser.Owner);
            ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);

            transform.Position = ownerTransform.Position;
            transform.Rotation = ownerTransform.Rotation;
        }
    }
}