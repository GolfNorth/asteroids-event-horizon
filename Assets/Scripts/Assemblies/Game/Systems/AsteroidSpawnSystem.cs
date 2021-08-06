using System.Linq;
using System.Numerics;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система респауна астероидов
    /// </summary>
    public sealed class AsteroidSpawnSystem : AutoSpawnSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<AsteroidComponent> _filter;

        public AsteroidSpawnSystem(Game game) : base(game, game.Settings.Asteroid.SpawnDelay)
        {
            _filter = new EcsFilter<AsteroidComponent>(World);
        }

        protected override void Restart()
        {
            base.Restart();

            uint[] entities = _filter.Entities.ToArray();

            foreach (uint entity in entities)
            {
                World.DestroyEntity(entity);
            }
        }

        protected override void Spawn()
        {
            uint entity = Game.Factory.CreateAsteroid();

            ref AsteroidComponent asteroid = ref World.GetComponent<AsteroidComponent>(entity);

            AsteroidSizeSettings sizeSettings = Game.Settings.Asteroid.SizeSettings[(int) asteroid.Size];

            float offset = sizeSettings.Offset;

            ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
            transform.Position = GetRandomPosition(offset);
        }
    }
}