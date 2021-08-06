namespace NonUnity.Game
{
    /// <summary>
    /// Система запуска игры
    /// </summary>
    public sealed class StartGameSystem : GameSystem, IUpdateSystem
    {
        public StartGameSystem(Game game) : base(game)
        {
        }

        public void Update(float dt)
        {
            if (Game.State == GameState.Play)
                return;

            if (Game.Command.Fire || Game.Command.AltFire)
            {
                Game.State = GameState.Play;
                Game.Score = 0;

                uint entity = World.CreateEntity();

                World.AddComponent<RestartComponent>(entity);
            }
        }
    }
}