using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Исполняемая система
    /// </summary>
    public abstract class ExecuteSystem : BaseSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter _filter;

        protected ExecuteSystem(Game game, EcsFilter filter = null) : base(game)
        {
            _filter = filter ?? new EcsFilter(World);
        }

        public virtual void Update(float dt)
        {
            foreach (uint entity in _filter.Entities)
            {
                Execute(entity, dt);
            }
        }

        /// <summary>
        /// Действие над сущностью
        /// </summary>
        protected abstract void Execute(uint entity, float dt);
    }

    /// <summary>
    /// Исполняемая система с однокомпонентным фильтром
    /// </summary>
    public abstract class ExecuteSystem<T> : ExecuteSystem
        where T : struct
    {
        protected ExecuteSystem(Game game) : base(game, new EcsFilter<T>(game.World))
        {
        }
    }

    /// <summary>
    /// Исполняемая система с двукомпонентным фильтром
    /// </summary>
    public abstract class ExecuteSystem<T1, T2> : ExecuteSystem
        where T1 : struct
        where T2 : struct
    {
        protected ExecuteSystem(Game game) : base(game, new EcsFilter<T1, T2>(game.World))
        {
        }
    }

    /// <summary>
    /// Исполняемая система с трехкомпонентным фильтром
    /// </summary>
    public abstract class ExecuteSystem<T1, T2, T3> : ExecuteSystem
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        protected ExecuteSystem(Game game) : base(game, new EcsFilter<T1, T2, T3>(game.World))
        {
        }
    }
}