using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Сервис команд
    /// </summary>
    public sealed class CommandService
    {
        /// <summary>
        /// Пространство сущностей
        /// </summary>
        private readonly EcsWorld _world;

        /// <summary>
        /// Сущность команд
        /// </summary>
        private readonly uint _entityId;

        /// <summary>
        /// Конструктор сервиса команд
        /// </summary>
        /// <param name="game">Геймплейный объект</param>
        public CommandService(Game game)
        {
            _world = game.World;
            _entityId = _world.CreateEntity();

            _world.AddComponent<CommandComponent>(_entityId);
        }

        /// <summary>
        /// Передвижение корабля
        /// </summary>
        public void Translate(float value)
        {
            ref CommandComponent commandComponent = ref GetComponent();

            commandComponent.Translation = value;
        }

        /// <summary>
        /// Поворот корабля
        /// </summary>
        public void Rotate(float value)
        {
            ref CommandComponent commandComponent = ref GetComponent();

            commandComponent.Rotation = value;
        }

        /// <summary>
        /// Выстрел корабля
        /// </summary>
        public void Fire(bool value)
        {
            ref CommandComponent commandComponent = ref GetComponent();

            commandComponent.Fire = value;
        }

        /// <summary>
        /// Альтернативный выстрел корабля
        /// </summary>
        public void AltFire(bool value)
        {
            ref CommandComponent commandComponent = ref GetComponent();

            commandComponent.AltFire = value;
        }

        /// <summary>
        /// Получить компонент команд
        /// </summary>
        private ref CommandComponent GetComponent()
        {
            return ref _world.GetComponent<CommandComponent>(_entityId);
        }
    }
}