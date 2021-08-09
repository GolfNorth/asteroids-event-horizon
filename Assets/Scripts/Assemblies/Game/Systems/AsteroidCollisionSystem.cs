namespace NonUnity.Game
{
    /// <summary>
    /// Система обработки столкновения астероида
    /// </summary>
    public sealed class AsteroidCollisionSystem : ExecuteSystem<AsteroidComponent, CollisionComponent>
    {
        public AsteroidCollisionSystem(Game game) : base(game)
        {
        }

        protected override void Execute(uint entity, float dt)
        {
            ref CollisionComponent collision = ref World.GetComponent<CollisionComponent>(entity);

            if (World.HasComponent<ShipComponent>(collision.Other))
                return;

            Game.Score++;

            World.AddComponent<DestroyComponent>(entity);

            if (World.HasComponent<LaserComponent>(collision.Other))
                return;

            CreateNext(entity);
        }

        /// <summary>
        /// Создать меньший астероид
        /// </summary>
        private void CreateNext(uint entity)
        {
            ref AsteroidComponent asteroid = ref World.GetComponent<AsteroidComponent>(entity);

            if (asteroid.Size == AsteroidSize.Small)
                return;

            ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);

            AsteroidSize newSize = asteroid.Size == AsteroidSize.Large ? AsteroidSize.Middle : AsteroidSize.Small;

            uint newEntity = Game.Factory.CreateAsteroid(newSize);

            ref AsteroidComponent newAsteroid = ref World.GetComponent<AsteroidComponent>(newEntity);
            newAsteroid.Size = newSize;

            ref TransformComponent newTransform = ref World.GetComponent<TransformComponent>(newEntity);
            newTransform.Position = transform.Position;
        }
    }
}