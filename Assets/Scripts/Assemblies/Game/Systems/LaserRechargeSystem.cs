using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система перезарядки лазера
    /// </summary>
    public sealed class LaserRechargeSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<LaserGunComponent> _filter;

        public LaserRechargeSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<LaserGunComponent>(World);
        }

        public void Update(float dt)
        {
            if (Game.State != GameState.Play)
                return;

            foreach (uint entity in _filter.Entities)
            {
                ref LaserGunComponent laserGun = ref World.GetComponent<LaserGunComponent>(entity);

                if (laserGun.Charges >= Game.Settings.Ship.LaserMaxCharges)
                    continue;

                if (laserGun.NextCharge > 0)
                {
                    laserGun.NextCharge -= dt;

                    if (laserGun.NextCharge > 0)
                        continue;
                }

                laserGun.Charges++;
                laserGun.NextCharge = Game.Settings.Ship.LaserCooldown;
            }
        }
    }
}