using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система лазерной пушки
    /// </summary>
    public sealed class LaserGunSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<LaserGunComponent> _filter;

        public LaserGunSystem(Game game) : base(game)
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

                if (laserGun.NextFire > 0)
                {
                    laserGun.NextFire -= dt;
                }

                if (laserGun.EndFire > 0)
                {
                    laserGun.EndFire -= dt;
                }

                if (!Game.Command.AltFire || laserGun.Charges == 0 || laserGun.NextFire > 0)
                    continue;

                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);

                uint laserEntity = Game.Factory.CreateLaser();

                ref LaserComponent laser = ref World.GetComponent<LaserComponent>(laserEntity);
                laser.Owner = entity;
                laser.EndFire = Game.Settings.Ship.LaserFireDuration;

                ref TransformComponent laserTransform = ref World.GetComponent<TransformComponent>(laserEntity);
                laserTransform.Position = transform.Position;
                laserTransform.Rotation = transform.Rotation;

                laserGun.Charges--;
                laserGun.NextFire = Game.Settings.Ship.LaserFireRate;
                laserGun.EndFire = Game.Settings.Ship.LaserFireDuration;
            }
        }
    }
}