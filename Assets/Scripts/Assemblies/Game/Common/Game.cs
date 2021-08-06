using System;
using System.Collections.Generic;
using System.Drawing;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Класс геймплейного объекта
    /// </summary>
    public sealed class Game
    {
        /// <summary>
        /// Конфигурация игры
        /// </summary>
        internal GameSettings Settings;

        /// <summary>
        /// Состояние инициализации игры
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// Инициализируемые системы
        /// </summary>
        private List<IInitSystem> _initSystems;

        /// <summary>
        /// Обновляемые системы
        /// </summary>
        private readonly List<IUpdateSystem> _updateSystems;

        /// <summary>
        /// Рандомизатор
        /// </summary>
        internal Random Random { get; }

        /// <summary>
        /// Фабрика сущностей
        /// </summary>
        internal EntityFactory Factory { get; }

        /// <summary>
        /// Пространство сущностей
        /// </summary>
        public EcsWorld World { get; }

        /// <summary>
        /// Сервис команд
        /// </summary>
        public CommandService Command { get; }

        /// <summary>
        /// Состояние игры
        /// </summary>
        public GameState State { get; internal set; }

        /// <summary>
        /// Резульат игры
        /// </summary>
        public int Score { get; internal set; }

        /// <summary>
        /// Игровые границы
        /// </summary>
        public RectangleF Bounds
        {
            get => Settings.Bounds;
            set => Settings.Bounds = value;
        }

        /// <summary>
        /// Конструктор объекта
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        /// <param name="settings">Конфигурация игры</param>
        /// <param name="viewFactory">Фабрика визуализаторов</param>
        internal Game(EcsWorld world, GameSettings settings, IViewFactory viewFactory)
        {
            World = world;
            Settings = settings;
            Random = new Random();
            Command = new CommandService();
            Factory = new EntityFactory(this, viewFactory);

            _initSystems = new List<IInitSystem>();
            _updateSystems = new List<IUpdateSystem>();

            AddSystem(new StartGameSystem(this));
            AddSystem(new ShipSpawnSystem(this));
            AddSystem(new AsteroidSpawnSystem(this));
            AddSystem(new UfoSpawnSystem(this));
            AddSystem(new ShipMovementSystem(this));
            AddSystem(new TransformSystem(this));
            AddSystem(new TeleportSystem(this));
            AddSystem(new ShapeSystem(this));
            AddSystem(new ViewMovementSystem(this));
            AddSystem(new CleanUpSystem(this));
        }

        /// <summary>
        /// Обновление геймплея
        /// </summary>
        /// <param name="dt">Прошедшее время</param>
        public void Update(float dt)
        {
            if (!_initialized)
            {
                foreach (IInitSystem system in _initSystems)
                {
                    system.Init();
                }

                _initSystems = null;
                _initialized = true;
            }

            foreach (IUpdateSystem system in _updateSystems)
            {
                system.Update(dt);
            }
        }

        /// <summary>
        /// Добавить систему
        /// </summary>
        /// <param name="system">Новая система</param>
        private void AddSystem(ISystem system)
        {
            if (system is IInitSystem initSystem)
            {
                _initSystems.Add(initSystem);
            }

            if (system is IUpdateSystem updateSystem)
            {
                _updateSystems.Add(updateSystem);
            }
        }
    }
}