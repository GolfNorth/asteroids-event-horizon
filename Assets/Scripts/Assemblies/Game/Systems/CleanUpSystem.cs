using System.Linq;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система очистки
    /// </summary>
    public sealed class CleanUpSystem : GameSystem, IUpdateSystem
    {
        private readonly EcsFilter<RestartComponent> _restartFilter;

        public CleanUpSystem(Game game) : base(game)
        {
            _restartFilter = new EcsFilter<RestartComponent>(World);
        }

        public void Update(float dt)
        {
            if (_restartFilter.Entities.Count > 0)
            {
                uint[] restartEntities = _restartFilter.Entities.ToArray();

                foreach (uint entity in restartEntities)
                {
                    World.RemoveComponent<RestartComponent>(entity);
                }
            }
        }
    }
}