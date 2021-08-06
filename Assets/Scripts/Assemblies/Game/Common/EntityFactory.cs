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
            _game.World.AddComponent<MachineGunComponent>(entityId);
            _game.World.AddComponent<LaserGunComponent>(entityId);

            ref TransformComponent transform = ref _game.World.AddComponent<TransformComponent>(entityId);
            transform.Offset = _game.Settings.Ship.Offset;

            ref MovementComponent movement = ref _game.World.AddComponent<MovementComponent>(entityId);
            movement.Direction = Vector2.UnitX;

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Layer = (byte) Layer.Ship;
            body.Mask = (byte) Layer.Enemy;
            body.Shape = new PolygonShape(_game.Settings.Ship.ShapeVertexes);

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateShip();
            view.Value.Activate();

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

            ref AsteroidComponent asteroid = ref _game.World.AddComponent<AsteroidComponent>(entityId);
            asteroid.Size = size;

            ref TransformComponent transform = ref _game.World.AddComponent<TransformComponent>(entityId);
            transform.Offset = sizeSettings.Offset;

            ref MovementComponent movement = ref _game.World.AddComponent<MovementComponent>(entityId);
            movement.Direction = Vector2.UnitX;

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Layer = (byte) Layer.Asteroid;
            body.Mask = (byte) Layer.Ship;
            body.Shape = new CircleShape(sizeSettings.Radius);

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateAsteroid(size);
            view.Value.Activate();

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

            ref TransformComponent transform = ref _game.World.AddComponent<TransformComponent>(entityId);
            transform.Offset = _game.Settings.Ufo.Offset;

            ref MovementComponent movement = ref _game.World.AddComponent<MovementComponent>(entityId);
            movement.Direction = Vector2.UnitX;

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Layer = (byte) Layer.Ufo;
            body.Mask = (byte) Layer.Ship;
            body.Shape = new PolygonShape(_game.Settings.Ufo.ShapeVertexes);

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateUfo();
            view.Value.Activate();

            return entityId;
        }

        /// <summary>
        /// Создать пулю
        /// </summary>
        public uint CreateBullet()
        {
            uint entityId = _game.World.CreateEntity();

            _game.World.AddComponent<BulletComponent>(entityId);

            ref TransformComponent transform = ref _game.World.AddComponent<TransformComponent>(entityId);
            transform.Offset = _game.Settings.Ship.BulletOffset;

            ref MovementComponent movement = ref _game.World.AddComponent<MovementComponent>(entityId);
            movement.Direction = Vector2.UnitX;

            ref BodyComponent body = ref _game.World.AddComponent<BodyComponent>(entityId);
            body.Layer = (byte) Layer.Ship;
            body.Mask = (byte) Layer.Enemy;
            body.Shape = new PointShape();

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateBullet();
            view.Value.Activate();

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
            body.Layer = (byte) Layer.Ship;
            body.Mask = (byte) Layer.Enemy;
            body.Shape = new EdgeShape(Vector2.Zero, new Vector2(_game.Settings.Bounds.Width, 0f));

            ref ViewComponent view = ref _game.World.AddComponent<ViewComponent>(entityId);
            view.Value = _viewFactory.CreateLaser();
            view.Value.Activate();

            return entityId;
        }
    }
}