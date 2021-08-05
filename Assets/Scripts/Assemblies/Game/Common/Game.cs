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
        /// Коллекция систем
        /// </summary>
        private readonly List<ISystem> _systems;

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
            Factory = new EntityFactory(this, viewFactory);

            _systems = new List<ISystem>
            {
                new ShipMovementSystem(this),
                new TransformSystem(this),
                new ViewMovementSystem(this)
            };

            Command = new CommandService(this);
        }

        /// <summary>
        /// Обновление геймплея
        /// </summary>
        /// <param name="dt">Прошедшее время</param>
        public void Update(float dt)
        {
            if (!_initialized)
            {
                foreach (ISystem system in _systems)
                {
                    if (system is IInitSystem initSystem)
                    {
                        initSystem.Init();
                    }
                }

                _initialized = true;
            }

            foreach (ISystem system in _systems)
            {
                if (system is IUpdateSystem updateSystem)
                {
                    updateSystem.Update(dt);
                }
            }
        }
    }
}