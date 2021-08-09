namespace NonUnity.Game
{
    /// <summary>
    /// Система перезарядки лазера
    /// </summary>
    public sealed class LaserRechargeSystem : ExecuteSystem<LaserGunComponent>
    {
        public LaserRechargeSystem(Game game) : base(game)
        {
        }

        public override void Update(float dt)
        {
            if (Game.State != GameState.Play)
                return;

            base.Update(dt);
        }

        protected override void Execute(uint entity, float dt)
        {
            ref LaserGunComponent laserGun = ref World.GetComponent<LaserGunComponent>(entity);

            if (laserGun.Charges >= Game.Settings.Ship.LaserMaxCharges)
                return;

            if (laserGun.NextCharge > 0)
            {
                laserGun.NextCharge -= dt;

                if (laserGun.NextCharge > 0)
                    return;
            }

            laserGun.Charges++;
            laserGun.NextCharge = Game.Settings.Ship.LaserCooldown;
        }
    }
}