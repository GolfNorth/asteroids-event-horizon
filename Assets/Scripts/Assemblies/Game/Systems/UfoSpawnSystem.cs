using System.Linq;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    public class UfoSpawnSystem : AutoSpawnSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<UfoComponent> _filter;

        public UfoSpawnSystem(Game game) : base(game, game.Settings.Ufo.SpawnDelay, game.Settings.Ufo.SpawnDelay)
        {
            _filter = new EcsFilter<UfoComponent>(World);
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
            uint entity = Game.Factory.CreateUfo();

            ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
            transform.Position = GetRandomPosition(Game.Settings.Ufo.Offset);
        }
    }
}