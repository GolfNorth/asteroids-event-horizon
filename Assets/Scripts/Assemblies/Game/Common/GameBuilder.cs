using System.Drawing;
using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Класс формирования экземпляра геймплейного объекта
    /// </summary>
    public sealed class GameBuilder
    {
        /// <summary>
        /// Фабрика визуализаторов
        /// </summary>
        private readonly IViewFactory _viewFactory;

        /// <summary>
        /// Пространство сущностей
        /// </summary>
        private EcsWorld _world;

        /// <summary>
        /// Игровые границы
        /// </summary>
        private RectangleF _bounds = new RectangleF(-16f, 9f, 32f, 18f);

        /// <summary>
        /// Конфигурация корабля
        /// </summary>
        private ShipSettings _shipSettings = new ShipSettings
        {
            AngularSpeed = ShipSettings.DefaultAngularSpeed,
            LinearSpeed = ShipSettings.DefaultLinearSpeed,
            StopSpeed = ShipSettings.DefaultStopSpeed,
            Inertia = ShipSettings.DefaultInertia,
            BulletSpeed = ShipSettings.DefaultBulletSpeed,
            BulletFireRate = ShipSettings.DefaultBulletFireRate,
            LaserFireRate = ShipSettings.DefaultLaserFireRate,
            LaserFireDuration = ShipSettings.DefaultLaserFireDuration,
            LaserMaxCharges = ShipSettings.DefaultLaserMaxCharges,
            LaserCooldown = ShipSettings.DefaultLaserCooldown,
            ShapeVertexes = ShipSettings.DefaultShapeVertexes
        };

        /// <summary>
        /// Конфигурация атероидов
        /// </summary>
        private AsteroidSettings _asteroidSettings = new AsteroidSettings
        {
            SpawnDelay = AsteroidSettings.DefaultSpawnDelay,
            SizeSettings = AsteroidSettings.DefaultSizeSettings
        };

        /// <summary>
        /// Конфигурация НЛО
        /// </summary>
        private UfoSettings _ufoSettings = new UfoSettings
        {
            SpawnDelay = UfoSettings.DefaultSpawnDelay,
            LinearSpeed = UfoSettings.DefaultLinearSpeed,
            ShapeVertexes = UfoSettings.DefaultShapeVertexes
        };

        /// <summary>
        /// Игровые границы
        /// </summary>
        /// <param name="viewFactory">Фабрика визуализаторов</param>
        public GameBuilder(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        /// <summary>
        /// Установить пространство сущностей
        /// </summary>
        /// <param name="world">Пространство сущностей</param>
        public GameBuilder SetWorld(EcsWorld world)
        {
            _world = world;

            return this;
        }

        /// <summary>
        /// Установить пространство сущностей
        /// </summary>
        /// <param name="bounds">Игровые границы</param>
        public GameBuilder SetBounds(in RectangleF bounds)
        {
            _bounds = bounds;

            return this;
        }

        /// <summary>
        /// Установить конфигурацию корабля
        /// </summary>
        /// <param name="shipSettings">Конфигурация корабля</param>
        public GameBuilder SetShipSettings(in ShipSettings shipSettings)
        {
            _shipSettings = shipSettings;

            return this;
        }

        /// <summary>
        /// Установить конфигурацию астероидов
        /// </summary>
        /// <param name="asteroidSettings">Конфигурация астероидов</param>
        public GameBuilder SetAsteroidSettings(in AsteroidSettings asteroidSettings)
        {
            _asteroidSettings = asteroidSettings;

            return this;
        }

        /// <summary>
        /// Установить конфигурацию НЛО
        /// </summary>
        /// <param name="ufoSettings">Конфигурация НЛО</param>
        public GameBuilder SetUfoSettings(in UfoSettings ufoSettings)
        {
            _ufoSettings = ufoSettings;

            return this;
        }

        /// <summary>
        /// Сформировать экземпляр геймплейного объекта
        /// </summary>
        public Game Build()
        {
            GameSettings gameSettings = new GameSettings
            {
                Bounds = _bounds,
                Ship = _shipSettings,
                Asteroid = _asteroidSettings,
                Ufo = _ufoSettings
            };

            return new Game(_world ?? new EcsWorld(), gameSettings, _viewFactory);
        }
    }
}