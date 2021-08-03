using System;
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
        internal readonly GameSettings Settings;

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
            Command = new CommandService(this);
        }
    }
}