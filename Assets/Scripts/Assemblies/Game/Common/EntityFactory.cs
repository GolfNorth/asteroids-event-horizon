using System;
using System.Numerics;
using NonUnity.Collision;

namespace NonUnity.Game
{
    /// <summary>
    /// Фабрика сущностей
    /// </summary>
    internal sealed class EntityFactory
    {
        /// <summary>
        /// Пространство сущностей
        /// </summary>
        private readonly Game _game;

        /// <summary>
        /// Фабрика визуализаторов
        /// </summary>
        private readonly IViewFactory _viewFactory;

        /// <summary>
        /// Конструктор фабрики сущностей
        /// </summary>
        /// <param name="game">Геймплейный объект</param>
        /// <param name="viewFactory">Фабрика визуализаторов</param>
        public EntityFactory(Game game, IViewFactory viewFactory)
        {
            _game = game;
            _viewFactory = viewFactory;
        }

        /// <summary>
        /// Создать корабль
        /// </summary>
        public uint CreateShip()
        {
            uint entityId = _game.World.CreateEntity();

            _game.World.AddComponent<ShipComponent>(entityId);
            _game.World.AddComponent<TransformComponent>(entityId);
            _game.World.AddComponent<MovementComponent>(entityId);
            _game.World.AddComponent<MachineGunComponent>(entityId);
            _game.World.AddComponent<LaserGunComponent>(entityId);

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Layer = (byte) Layer.Ship;
            body.Mask = (byte) Layer.Enemy;
            body.Shape = new PolygonShape(_game.Settings.Ship.ShapeVertexes);

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateShip();

            return entityId;
        }

        /// <summary>
        /// Создать астероид
        /// </summary>
        /// <param name="size">Размер астероида</param>
        public uint CreateAsteroid(AsteroidSize size = AsteroidSize.Random)
        {
            uint entityId = _game.World.CreateEntity();

            if (size == AsteroidSize.Random)
            {
                size = (AsteroidSize) _game.Random.Next(1, Enum.GetValues(typeof(AsteroidSize)).Length - 1);
            }

            AsteroidSizeSettings sizeSettings = _game.Settings.Asteroid.SizeSettings[(int) size];

            _game.World.AddComponent<TransformComponent>(entityId);

            ref AsteroidComponent asteroidComponent = ref _game.World.AddComponent<AsteroidComponent>(entityId);
            asteroidComponent.Size = size;

            ref MovementComponent movementComponent = ref _game.World.AddComponent<MovementComponent>(entityId);
            movementComponent.Speed = sizeSettings.MinSpeed +
                                      (float) _game.Random.NextDouble() *
                                      (sizeSettings.MaxSpeed - sizeSettings.MinSpeed);

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Layer = (byte) Layer.Asteroid;
            body.Mask = (byte) Layer.Ship;
            body.Shape = new CircleShape(sizeSettings.Radius);

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateAsteroid(size);

            return entityId;
        }

        /// <summary>
        /// Создать НЛО
        /// </summary>
        public uint CreateUfo()
        {
            uint entityId = _game.World.CreateEntity();

            _game.World.AddComponent<UfoComponent>(entityId);
            _game.World.AddComponent<TransformComponent>(entityId);
            _game.World.AddComponent<MovementComponent>(entityId);

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Layer = (byte) Layer.Ufo;
            body.Mask = (byte) Layer.Ship;
            body.Shape = new PolygonShape(_game.Settings.Ufo.ShapeVertexes);

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateUfo();

            return entityId;
        }

        /// <summary>
        /// Создать пулю
        /// </summary>
        public uint CreateBullet()
        {
            uint entityId = _game.World.CreateEntity();

            _game.World.AddComponent<BulletComponent>(entityId);
            _game.World.AddComponent<TransformComponent>(entityId);
            _game.World.AddComponent<MovementComponent>(entityId);

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Shape = new PointShape();

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateBullet();

            return entityId;
        }

        /// <summary>
        /// Создать лазер
        /// </summary>
        public uint CreateLaser()
        {
            uint entityId = _game.World.CreateEntity();

            _game.World.AddComponent<LaserComponent>(entityId);
            _game.World.AddComponent<TransformComponent>(entityId);

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Shape = new EdgeShape(Vector2.Zero, new Vector2(_game.Settings.Bounds.Width, 0f));

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateLaser();

            return entityId;
        }
    }
}