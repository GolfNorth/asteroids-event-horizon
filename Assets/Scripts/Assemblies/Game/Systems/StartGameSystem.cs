using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система запуска игры
    /// </summary>
    public sealed class StartGameSystem : GameSystem, IInitSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр комманд
        /// </summary>
        private EcsFilter<CommandComponent> _commandFilter;

        /// <summary>
        /// Сущность команд
        /// </summary>
        private uint _commandEntity;

        public StartGameSystem(Game game) : base(game)
        {
            _commandFilter = new EcsFilter<CommandComponent>(World);
        }

        public void Init()
        {
            foreach (uint entityId in _commandFilter.Entities)
            {
                _commandEntity = entityId;
            }

            _commandFilter.Remove();
            _commandFilter = null;
        }

        public void Update(float dt)
        {
            if (Game.State == GameState.Play)
                return;

            ref CommandComponent command = ref World.GetComponent<CommandComponent>(_commandEntity);

            if (command.Fire || command.AltFire)
            {
                Game.State = GameState.Play;
                uint entity = World.CreateEntity();

                World.AddComponent<RestartComponent>(entity);
            }
        }
    }
}