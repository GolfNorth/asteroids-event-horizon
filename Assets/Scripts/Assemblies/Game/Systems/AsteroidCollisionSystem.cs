using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система обработки столкновения астероида
    /// </summary>
    public sealed class AsteroidCollisionSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<AsteroidComponent, CollisionComponent> _filter;

        public AsteroidCollisionSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<AsteroidComponent, CollisionComponent>(World);
        }

        public void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                ref CollisionComponent collision = ref World.GetComponent<CollisionComponent>(entity);

                if (World.HasComponent<ShipComponent>(collision.Other))
                    continue;

                Game.Score++;

                World.AddComponent<DestroyComponent>(entity);

                if (World.HasComponent<LaserComponent>(collision.Other))
                    continue;

                CreateNext(entity);
            }
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