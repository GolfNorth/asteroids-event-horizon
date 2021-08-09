using System.Linq;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система деактивации объектов
    /// </summary>
    public sealed class DeactivateSystem : BaseSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<DeactivateComponent> _filter;

        public DeactivateSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<DeactivateComponent>(World);
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

                        view.Value.Deactivate();
                    }

                    World.DestroyEntity(entity);
                }
            }
        }
    }
}