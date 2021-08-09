using System.Linq;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система уничтожения сущностей
    /// </summary>
    public sealed class DestroySystem :BaseSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр компонента уничтожения
        /// </summary>
        private readonly EcsFilter<DestroyComponent> _filter;

        public DestroySystem(Game game) : base(game)
        {
            _filter = new EcsFilter<DestroyComponent>(World);
        }

        public void Update(float dt)
        {
            if (_filter.Entities.Count > 0)
            {
                uint[] entities = _filter.Entities.ToArray();

                foreach (uint entity in entities)
                {
                    if (World.HasComponent<ViewComponent>(entity))
                    {
                        ref ViewComponent view = ref World.GetComponent<ViewComponent>(entity);

                        view.Value.Destroy();
                    }

                    World.DestroyEntity(entity);
                }
            }
        }
    }
}