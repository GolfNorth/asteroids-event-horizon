namespace NonUnity.Game
{
    /// <summary>
    /// Система респауна корабля
    /// </summary>
    public sealed class ShipSpawnSystem : SpawnSystem
    {
        public ShipSpawnSystem(Game game) : base(game)
        {
        }

        protected override void Restart()
        {
            Game.Factory.CreateShip();
        }
    }
}