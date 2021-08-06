using Asteroids.Extensions;
using NonUnity.Game;
using UnityEngine;

namespace Asteroids.Data
{
    /// <summary>
    /// Данные корабля
    /// </summary>
    [CreateAssetMenu(fileName = "ShipData", menuName = "Data/ShipData", order = 0)]
    public sealed class ShipData : SettingsData<ShipSettings>
    {
        [SerializeField, Tooltip("Вершины формы корабля")]
        private Vector2[] shapeVertexes = new Vector2[0];

        public override ref ShipSettings GetData()
        {
            settings.ShapeVertexes = shapeVertexes.Convert();

            return ref settings;
        }

        public override void Default()
        {
            settings.Offset = ShipSettings.DefaultOffset;
            settings.AngularSpeed = ShipSettings.DefaultAngularSpeed;
            settings.LinearSpeed = ShipSettings.DefaultLinearSpeed;
            settings.StopSpeed = ShipSettings.DefaultStopSpeed;
            settings.Inertia = ShipSettings.DefaultInertia;
            settings.BulletOffset = ShipSettings.DefaultBulletOffset;
            settings.BulletSpeed = ShipSettings.DefaultBulletSpeed;
            settings.BulletFireRate = ShipSettings.DefaultBulletFireRate;
            settings.LaserFireRate = ShipSettings.DefaultLaserFireRate;
            settings.LaserFireDuration = ShipSettings.DefaultLaserFireDuration;
            settings.LaserMaxCharges = ShipSettings.DefaultLaserMaxCharges;
            settings.LaserCooldown = ShipSettings.DefaultLaserCooldown;
            shapeVertexes = ShipSettings.DefaultShapeVertexes.Convert();
        }
    }
}