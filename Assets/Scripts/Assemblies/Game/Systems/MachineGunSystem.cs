﻿using NonUnity.Ecs;

namespace NonUnity.Game
{
    /// <summary>
    /// Система пулемета
    /// </summary>
    public sealed class MachineGunSystem : GameSystem, IUpdateSystem
    {
        /// <summary>
        /// Фильтр сущностей
        /// </summary>
        private readonly EcsFilter<MachineGunComponent> _filter;

        public MachineGunSystem(Game game) : base(game)
        {
            _filter = new EcsFilter<MachineGunComponent>(World);
        }

        public void Update(float dt)
        {
            if (Game.State != GameState.Play)
                return;

            foreach (uint entity in _filter.Entities)
            {
                ref MachineGunComponent machineGun = ref World.GetComponent<MachineGunComponent>(entity);

                if (machineGun.NextFire > 0)
                {
                    machineGun.NextFire -= dt;
                }

                if (!Game.Command.Fire || IsLaserFire(entity) || machineGun.NextFire > 0)
                    continue;

                ref TransformComponent transform = ref World.GetComponent<TransformComponent>(entity);
                ref MovementComponent movement = ref World.GetComponent<MovementComponent>(entity);

                uint bulletEntity = Game.Factory.CreateBullet();

                ref BulletComponent bullet = ref World.GetComponent<BulletComponent>(bulletEntity);
                bullet.Owner = entity;

                ref TransformComponent bulletTransform = ref World.GetComponent<TransformComponent>(bulletEntity);
                bulletTransform.Position = transform.Position;
                bulletTransform.Rotation = transform.Rotation;

                ref MovementComponent bulletMovement = ref World.GetComponent<MovementComponent>(bulletEntity);
                bulletMovement.Direction = movement.Direction;
                bulletMovement.Velocity = movement.Direction * Game.Settings.Ship.BulletSpeed;

                machineGun.NextFire = Game.Settings.Ship.BulletFireRate;
            }
        }

        /// <summary>
        /// Производится ли выстрел лазера
        /// </summary>
        private bool IsLaserFire(uint entity)
        {
            if (!World.HasComponent<LaserGunComponent>(entity))
                return false;

            ref LaserGunComponent laserGun = ref World.GetComponent<LaserGunComponent>(entity);

            return laserGun.EndFire > 0;
        }
    }
}