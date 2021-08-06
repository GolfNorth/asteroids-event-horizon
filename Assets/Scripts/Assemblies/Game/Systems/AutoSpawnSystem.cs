using Vector2 = System.Numerics.Vector2;

namespace NonUnity.Game
{
    /// <summary>
    /// Система автоматического респауна
    /// </summary>
    public abstract class AutoSpawnSystem : SpawnSystem
    {
        /// <summary>
        /// Время между появлениями
        /// </summary>
        private readonly float _spawnDelay;

        /// <summary>
        /// Время перед первым появлением
        /// </summary>
        private readonly float _spawnOffset;

        /// <summary>
        /// Оставшееся время
        /// </summary>
        private float _timeLeft;

        protected AutoSpawnSystem(Game game, float spawnDelay, float spawnOffset = 0) : base(game)
        {
            _spawnDelay = spawnDelay;
            _spawnOffset = spawnOffset;
            _timeLeft = _spawnOffset;
        }

        protected override void Restart()
        {
            _timeLeft = _spawnOffset;
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            if (Game.State != GameState.Play)
                return;

            _timeLeft -= dt;

            if (_timeLeft < 0)
            {
                Spawn();

                _timeLeft = _spawnDelay;
            }
        }

        /// <summary>
        /// Спаун сущности
        /// </summary>
        protected abstract void Spawn();

        /// <summary>
        /// Возвращает случайную позицию на игровой границе
        /// </summary>
        /// <param name="offset">Отступ от игровой границы</param>
        protected Vector2 GetRandomPosition(float offset = 0)
        {
            Vector2 position = new Vector2();
            int side = Game.Random.Next(0, 4);

            switch (side)
            {
                case 0:
                    position.X = Game.Bounds.Left + (float) Game.Random.NextDouble() * Game.Bounds.Width;
                    position.Y = Game.Bounds.Top + offset;
                    break;
                case 1:
                    position.X = Game.Bounds.Left + (float) Game.Random.NextDouble() * Game.Bounds.Width;
                    position.Y = Game.Bounds.Bottom - offset;
                    break;
                case 2:
                    position.X = Game.Bounds.Left - offset;
                    position.Y = Game.Bounds.Bottom + (float) Game.Random.NextDouble() * Game.Bounds.Height;
                    break;
                case 3:
                    position.X = Game.Bounds.Right + offset;
                    position.Y = Game.Bounds.Bottom + (float) Game.Random.NextDouble() * Game.Bounds.Height;
                    break;
            }

            return position;
        }
    }
}